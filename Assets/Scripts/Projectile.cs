using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private RangeWeapon rangeWeapon;
    private Rigidbody2D rb2d;

    public void Init(RangeWeapon rangeWeapon)
    {
        this.rangeWeapon = rangeWeapon;
    }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb2d.velocity = transform.right * rangeWeapon.ProjectileSpeed;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Fish fish))
        {
            fish.Rb2d.AddForce(transform.right * rangeWeapon.EnemyPushForce, ForceMode2D.Impulse);
        }
    }
}
