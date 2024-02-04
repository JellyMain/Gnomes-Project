using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : PredatorFish
{
    [SerializeField] float bitePushForce = 3f;


    public override void Attack(Fish fish)
    {
        Rb2d.AddForce(transform.right * bitePushForce, ForceMode2D.Impulse);
        fish.Hp.TakeDamage(damage);
    }


}
