using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int maxHealth = 100;
    public bool destroyOnDeath = true;

    public int health { get; private set; }

    private void Start()
    {
        health = maxHealth;
    }

    public void Damage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            if (destroyOnDeath)
                Destroy(gameObject);
        }
    }
}
