using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static event Action OnNewWeaponCollected;

    private Inventory inventory;
    private Weapon currentWeapon;


    private void Awake()
    {
        inventory = GetComponent<Inventory>();
    }



    private void SetNewWeapon(Weapon newWeapon)
    {
        currentWeapon = newWeapon;
    }


    public Weapon GetCurrentWeapon()
    {
        return currentWeapon;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Item item))
        {
            if (inventory.hasEmptySlots())
            {
                inventory.AddItem(item.ItemSO);
                Destroy(other.gameObject);
            }
        }


        if (other.TryGetComponent(out Weapon weapon))
        {
            SetNewWeapon(weapon);
        }
    }
}
