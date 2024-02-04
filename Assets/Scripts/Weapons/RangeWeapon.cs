using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangeWeapon : Weapon
{
    [SerializeField] protected Projectile projectile;
    [SerializeField] float projectileSpeed = 10;
    [SerializeField] protected Transform shootingPoint;


    public float ProjectileSpeed => projectileSpeed;
    public float EnemyPushForce => enemyPushForce;



}
