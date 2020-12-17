using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : NetworkBehaviour {
    public int maxHealth = 100;
    public bool destroyOnDeath = true;

    [SyncVar]
    private int _health;

    public int health { get => _health; }

    private void Start() {
        if (!isServer) {
            return;
        }

        _health = maxHealth;
    }

    public void Damage(int amount) {
        if (!isServer) {
            return;
        }

        _health -= amount;

        if (health <= 0) {
            if (destroyOnDeath)
                Destroy(gameObject);
        }
    }
}
