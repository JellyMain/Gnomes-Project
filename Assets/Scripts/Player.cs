using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    private HP hp;
    private Inventory inventory;
    private ArmsMovement armsMovement;

    public ArmsMovement ArmsMovement => armsMovement;
    public HP Hp => hp;


    public static Player Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        hp = new HP(maxHealth);
        inventory = GetComponent<Inventory>();
        armsMovement = GetComponent<ArmsMovement>();
    }

    //Test
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hp.TakeDamage(10);
        }
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
