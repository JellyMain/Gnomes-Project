using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearWeapon : MelleWeapon
{
    [SerializeField] float playerPushForce = 3;


    public override void Attack()
    {
        PushPlayer();
    }


    private void PushPlayer()
    {
        Gnome.Rb2d.AddForce(Gnome.ArmsMovement.LookDirection * playerPushForce, ForceMode2D.Impulse);
    }
}
