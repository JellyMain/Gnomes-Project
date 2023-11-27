using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnome : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    private Rigidbody2D rb2d;
    private HP hp;
    private Inventory inventory;
    private ArmsMovement armsMovement;
    private WeaponInventory weaponInventory;

    public ArmsMovement ArmsMovement => armsMovement;
    public HP Hp => hp;
    public Rigidbody2D Rb2d => rb2d;


    public static Gnome Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        rb2d = GetComponent<Rigidbody2D>();
        weaponInventory = GetComponent<WeaponInventory>();
        inventory = GetComponent<Inventory>();
        armsMovement = GetComponent<ArmsMovement>();
        hp = new HP(maxHealth);
    }


    private void OnEnable()
    {
        GameInput.OnAttackAction += Attack;
    }


    private void OnDisable()
    {
        GameInput.OnAttackAction -= Attack;
    }


    //Test
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hp.TakeDamage(10);
        }
    }


    private void Attack()
    {
        weaponInventory.CurrentWeapon.Attack();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Item item))
        {
            if (inventory.hasEmptySlots() && item.CanBePicked)
            {
                inventory.AddItem(item.ItemSO);
                Destroy(other.gameObject);
            }
        }
    }
}
