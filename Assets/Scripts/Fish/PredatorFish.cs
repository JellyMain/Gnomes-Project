using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class PredatorFish : Fish
{
    public event Action OnAttacked;

    [Header("Hunting Settings")]

    [SerializeField] FishType preyType;
    [SerializeField] HuntingBehavior huntingBehavior;
    [SerializeField, Range(1, 15)] float preySearchRadius = 1.5f;
    [SerializeField] float losePreyTime = 2f;

    [Header("Attack Settings")]

    [SerializeField, Range(0, 5)] float attackRadius = 3f;
    [SerializeField] Transform attackPoint;
    [SerializeField] protected float damage = 10;
    [SerializeField] float attackCooldown = 1f;
    [SerializeField] float targetPushForce = 4f;

    [Header("Movement Settings")]

    [SerializeField] float circularMovementRadius = 5f;
    [SerializeField] float circularMovementTime = 5f;
    [SerializeField] float circularMovementSpeed = 2f;

    private Fish currentPreyFish;
    private bool canAttack = true;
    private bool isCirculing = false;
    private float losePreyTimer = 0;
    private float circularMovementTimer = 0;

    public Fish CurrentPreyFish => currentPreyFish;


    protected override void Start()
    {
        base.Start();
    }


    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (CurrentBehaviorState == BehaviorState.Hunting)
        {
            if (isCirculing)
            {
                MoveInCircles();
            }
            else
            {
                Move(huntingBehavior.CalculateMove(this, currentPreyFish), MaxMoveSpeed);
            }

            CheckForAttack();
        }
        FindPrey();
    }


    public abstract void Attack(Fish fish);


    private void CheckForAttack()
    {
        Collider2D targetCollider = Physics2D.OverlapCircle(attackPoint.position, attackRadius);

        if (targetCollider == null) return;

        if (canAttack && targetCollider.TryGetComponent(out Fish fish))
        {
            Attack(fish);
            OnAttacked?.Invoke();
            StartAttackCooldown();
            fish.SetThreatObject(transform);
            isCirculing = true;
        }
    }


    private void FindPrey()
    {
        Collider2D foundObject = Physics2D.OverlapCircle(transform.position, preySearchRadius);
        if (foundObject == null && currentPreyFish != null)
        {
            LosePrey();
            return;
        }

        if (foundObject == null || currentPreyFish != null) return;


        if (foundObject.TryGetComponent(out Fish fish))
        {
            IdentifyPrey(fish);
        }
    }


    private void LosePrey()
    {
        losePreyTimer += Time.deltaTime;
        if (losePreyTimer > losePreyTime)
        {
            ChangeBehaviorState(BehaviorState.Peaceful);
            currentPreyFish = null;
            losePreyTimer = 0;
        }
    }



    private void IdentifyPrey(Fish fish)
    {
        if (fish.FishType == preyType)
        {
            if (currentPreyFish == null)
            {
                currentPreyFish = fish;
                ChangeBehaviorState(BehaviorState.Hunting);
            }
        }
    }



    private void MoveInCircles()
    {
        if (currentPreyFish == null)
        {
            isCirculing = false;
            return;
        }

        if (isCirculing)
        {
            circularMovementTimer += Time.deltaTime;
            if (circularMovementTimer >= circularMovementTime)
            {
                isCirculing = false;
                circularMovementTimer = 0;
            }
            Move(GetDirectionToUnitCirclePoint(currentPreyFish.transform.position), MaxMoveSpeed);
        }
    }



    private Vector2 GetDirectionToUnitCirclePoint(Vector2 center)
    {
        Vector2 pointOnCircle = new Vector2(Mathf.Cos(circularMovementTimer * circularMovementSpeed), Mathf.Sin(circularMovementTimer * circularMovementSpeed)) * circularMovementRadius;
        Vector2 nextPoint = pointOnCircle + center;

        Vector2 direction = (nextPoint - (Vector2)transform.position).normalized;

        return direction;
    }


    private async void StartAttackCooldown()
    {
        canAttack = false;
        await Task.Delay((int)(attackCooldown * 1000));
        canAttack = true;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, preySearchRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
