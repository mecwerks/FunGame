using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : NetworkBehaviour
{
    public int maxHealth = 100;
    public bool destroyOnDeath = true;

    public int health { get; private set; }

    private void Start()
    {
        if (!isServer)
        {
            return;
        }

        health = maxHealth;
    }

    public void Damage(int amount)
    {
        if (!isServer)
        {
            return;
        }

        health -= amount;

        if (health <= 0)
        {
            if (destroyOnDeath)
                Destroy(gameObject);
        }
    }
}
