using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] float damage = 10;
    [SerializeField] float attackCooldown = 1f;
    [SerializeField] float enemyPushForce = 4f;
    [SerializeField] Collider2D attackCollider;
    [SerializeField] Collider2D pickUpCollider;
    [SerializeField] Transform rightArmGrip;
    [SerializeField] Transform leftArmGrip;
    [SerializeField] protected Animator animator;

    protected bool canAttack = true;
    protected Gnome player;
    protected Collider2D AttackCollider => attackCollider;
    protected float AttackCooldown => attackCooldown;

    public Transform RightArmGrip => rightArmGrip;
    public Transform LeftArmGrip => leftArmGrip;
    public float Damage => damage;




    private void Start()
    {
        player = Gnome.Instance;
    }


    public void TogglePickUpCollider(bool state)
    {
        pickUpCollider.enabled = state;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Fish fish))
        {
            fish.Rb2d.AddForce(Gnome.Instance.ArmsMovement.LookDirection * enemyPushForce, ForceMode2D.Impulse);
        }
    }


    public abstract void Attack();





}