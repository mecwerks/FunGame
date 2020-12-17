using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHurt : MonoBehaviour {
    public int damage = 25;
    public float delay = 1f;

    float damageTimer = 0;

    private void Update() {
        damageTimer -= Time.deltaTime;
    }

    private void OnTriggerStay(Collider other) {
        // this will only work for one player, need to do this either on player
        // or store a list of objects here
        if (damageTimer <= 0 && other.CompareTag("Player")) {
            Damageable dmgable = other.GetComponent<Damageable>();

            if (dmgable && dmgable.health > 0)
                dmgable.Damage(damage);

            damageTimer = delay;
        }
    }
}
