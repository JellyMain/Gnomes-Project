using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MelleWeapon : Weapon
{
    [SerializeField] Collider2D attackCollider;

    protected Collider2D AttackCollider => attackCollider;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Fish fish))
        {
            fish.Rb2d.AddForce(Gnome.Instance.ArmsMovement.LookDirection * enemyPushForce, ForceMode2D.Impulse);
        }
    }



}