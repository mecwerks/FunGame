using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FloorManager : NetworkBehaviour {
    public Damageable enemy;
    public FloorChangePortal portal;

    void Update() {
        if (isServer) {
            if (enemy.health <= 0) {
                portal.active = true;
            } else {
                portal.active = false;
            }
        }
    }
}
