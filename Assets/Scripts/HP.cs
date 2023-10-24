using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    [SerializeField] int health = 6;
    public int Health => health;


    public void TakeDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            if (health <= 0)
            {
                Die();
            }

        }
    }



    private void Heal(int healAmount)
    {
        if (health > 0)
        {
            health += healAmount;
        }
    }


    private void Die()
    {
        Destroy(gameObject);
    }

}
