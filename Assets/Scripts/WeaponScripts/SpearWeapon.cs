using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearWeapon : Weapon
{
    [SerializeField] float playerPushForce = 3;


    public override void Attack()
    {
        PushPlayer();
    }


    private void PushPlayer()
    {
        player.Rb2d.AddForce(player.ArmsMovement.LookDirection * playerPushForce, ForceMode2D.Impulse);
    }
}
