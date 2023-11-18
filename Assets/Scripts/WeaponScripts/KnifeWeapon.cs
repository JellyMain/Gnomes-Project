using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class KnifeWeapon : Weapon
{
    [SerializeField] float playerPushForce = 3;

    private const string ATTACKED = "Attacked";

    public override void Attack()
    {
        if (canAttack)
        {
            PushPlayer();
            animator.SetTrigger(ATTACKED);
            ToggleAttackCollider();
        }
    }


    private void PushPlayer()
    {
        player.Rb2d.AddForce(player.ArmsMovement.LookDirection * playerPushForce, ForceMode2D.Impulse);
    }


    private async void ToggleAttackCollider()
    {
        AttackCollider.enabled = true;
        canAttack = false;
        await Task.Delay((int)(AttackCooldown * 1000));
        AttackCollider.enabled = false;
        canAttack = true;
    }
}
