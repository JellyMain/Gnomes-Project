using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartFish : Fish
{
    public override void NonContextMove()
    {
        Vector2 randomDirection = new Vector2(UnityEngine.Random.Range(-1, 2), UnityEngine.Random.Range(0, 2)).normalized;

        Rb2d.velocity = randomDirection * StandartMoveSpeed;


        float angle = Mathf.Atan2(randomDirection.y, randomDirection.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, RotationSpeed * Time.deltaTime);

    }
}
