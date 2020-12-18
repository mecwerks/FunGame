using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FloorManager : NetworkBehaviour {
    public Damageable[] enemies;
    public FloorChangePortal portal;

    void Update() {
        if (isServer) {
            bool enemyAlive = false;

            foreach (var enemy in enemies) {
                if (enemy.health > 0) {
                    enemyAlive = true;
                    break;
                }
            }

            if (!enemyAlive) {
                portal.active = true;
            }
        }
    }
}
