using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float damage = 10;
    [SerializeField] protected float attackCooldown = 1f;
    [SerializeField] protected float enemyPushForce = 4f;
    [SerializeField] protected Collider2D pickUpCollider;
    [SerializeField] protected Transform rightArmGrip;
    [SerializeField] protected Transform leftArmGrip;
    [SerializeField] protected Animator animator;
    private Gnome gnome;

    protected bool canAttack = true;
    protected float AttackCooldown => attackCooldown;

    public Transform RightArmGrip => rightArmGrip;
    public Transform LeftArmGrip => leftArmGrip;
    public float Damage => damage;
    public Gnome Gnome => gnome;


    private void Start()
    {
        gnome = Gnome.Instance;
    }

    public void TogglePickUpCollider(bool state)
    {
        pickUpCollider.enabled = state;
    }


    public abstract void Attack();


}
