using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonWeapon : RangeWeapon
{
    public override void Attack()
    {
        Projectile shotProjectile = Instantiate(projectile, shootingPoint.position, transform.rotation);
        shotProjectile.Init(this);
    }
}
