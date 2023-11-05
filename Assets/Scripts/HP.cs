using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP
{
    public event Action<float> OnDamaged;
    public event Action<float> OnHealed;


    private float maxHealth = 100;
    private float currentHealth;
    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;




    public HP(float maxHealth)
    {
        this.maxHealth = maxHealth;
        SetHealth();
    }


    private void SetHealth()
    {
        currentHealth = maxHealth;
    }


    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            OnDamaged?.Invoke(currentHealth);
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
            }

        }
    }



    private void Heal(int healAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth += healAmount;
            OnHealed?.Invoke(currentHealth);
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;

            }
        }
    }


    private void Die()
    {

    }

}
