using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MeleeWeapon : MonoBehaviour {
    public int damage = 25;
    public PlayerAttack attack;

    void OnTriggerEnter(Collider col) {
        attack.Hit(col, damage);
    }
}
