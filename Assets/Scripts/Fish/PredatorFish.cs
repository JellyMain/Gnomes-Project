using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class PredatorFish : Fish
{
    public event Action OnAttacked;
    [SerializeField] float damage = 10;
    [SerializeField] float attackCooldown = 1f;
    [SerializeField] float targetPushForce = 4f;
    [SerializeField] Collider2D attackCollider;
    [SerializeField, Range(0, 1)] float attackRadiusMultiplier = 0.3f;
    private float attackRadius;
    private bool canAttack = false;


    private void Start()
    {
        attackRadius = NeighborRadius * attackRadiusMultiplier;
        Debug.Log(attackRadius);
    }


    private void Update()
    {
        CheckForAttack();
    }


    public abstract void Attack();



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Gnome player))
        {
            player.Hp.TakeDamage(damage);
            OnAttacked?.Invoke();
        }
    }



    private void CheckForAttack()
    {
        Collider2D targetCollider = Physics2D.OverlapCircle(transform.position, attackRadius);
        if (targetCollider != null && targetCollider != FishCollider)
        {
            Attack();
            OnAttacked?.Invoke();
            StartAttackCooldown();
        }
    }


    private void PrepareForAttack()
    {
        attackCollider.enabled = true;
        canAttack = true;
    }


    private async void StartAttackCooldown()
    {
        attackCollider.enabled = true;
        canAttack = false;
        await Task.Delay((int)(attackCooldown * 1000));
        attackCollider.enabled = false;
        canAttack = true;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
