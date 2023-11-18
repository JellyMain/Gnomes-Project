using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Fish : MonoBehaviour
{
    [SerializeField, Range(1, 5)] float standartMoveSpeed = 5f;
    [SerializeField, Range(5, 10)] float maxMoveSpeed = 10f;
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] Item lootItem;
    [SerializeField] float lootItemPushForce = 5;
    [SerializeField] float maxHp = 50;
    private Rigidbody2D rb2d;
    private HP hp;

    public HP Hp => hp;
    public Rigidbody2D Rb2d => rb2d;
    public float StandartMoveSpeed => standartMoveSpeed;
    public float MaxMoveSpeed => maxMoveSpeed;
    public float RotationSpeed => rotationSpeed;


    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        hp = new HP(50);
    }


    private void OnEnable()
    {
        hp.OnDead += DropLoot;
    }



    private void OnDisable()
    {
        hp.OnDead -= DropLoot;
    }



    private void FixedUpdate()
    {
        NonContextMove();
    }



    private void DropLoot()
    {
        Item spawnedLootItem = Instantiate(lootItem, transform.position, Quaternion.identity);
        Vector2 randomDirection = new Vector2(Random.Range(-1, 2f), Random.Range(-1, 2f));
        spawnedLootItem.Rb2d.AddForce(randomDirection * lootItemPushForce, ForceMode2D.Impulse);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Weapon weapon))
        {
            hp.TakeDamage(weapon.Damage);
        }
    }


    public abstract void NonContextMove();
}
