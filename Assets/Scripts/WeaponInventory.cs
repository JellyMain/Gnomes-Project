using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    public static event Action<Weapon> OnWeaponSet;

    [SerializeField] Transform weaponContainer;
    [SerializeField] Weapon startWeapon;
    private Weapon currentWeapon;

    public Weapon CurrentWeapon => currentWeapon;


    private void Start()
    {
        SetStartWeapon(startWeapon);
    }


    private void SetStartWeapon(Weapon weapon)
    {
        Weapon spawnedWeapon = Instantiate(weapon, weaponContainer);
        SetNewWeapon(spawnedWeapon);
    }


    private void SetNewWeapon(Weapon weapon)
    {
        ClearWeapon();
        currentWeapon = weapon;
        weapon.transform.SetParent(weaponContainer);
        weapon.transform.localPosition = Vector3.zero;
        weapon.TogglePickUpCollider(false);
        OnWeaponSet?.Invoke(currentWeapon);
    }


    private void ClearWeapon()
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Weapon weapon))
        {
            SetNewWeapon(weapon);
        }
    }




}
