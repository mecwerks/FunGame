using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : NetworkBehaviour {
    public Transform Cam;
    public GameObject Projectile;
    public Transform ProjectilePosition;
    public float AttackCooldown = 0.5f;
    public LayerMask enemyHitMask;

    private NetworkAnimator animator;
    private PlayerMovement movement;
    private Damageable dmgable;
    [SyncVar]
    private float attackTime = 0;

    private void Start() {
        if (!isLocalPlayer) {
            return;
        }

        animator = GetComponent<NetworkAnimator>();
        movement = GetComponent<PlayerMovement>();
        dmgable = GetComponent<Damageable>();
    }

    private void Update() {
        if (!isLocalPlayer) {
            return;
        }

        attackTime -= Time.deltaTime;

        if (dmgable.health <= 0) {
            return;
        }

        if (attackTime > 0) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            FireProjectile();
            movement.StrafeMode();
        } else if (Input.GetKeyDown(KeyCode.Mouse1)) {
            animator.SetTrigger("Attack");
            Stab();
        }
    }

    [Command]
    void FireProjectile() {
        // need to make this go towards where the camera is pointing
        GameObject go = Instantiate(Projectile, ProjectilePosition.position, ProjectilePosition.rotation);
        NetworkServer.Spawn(go);
        attackTime = AttackCooldown;
    }

    [Command]
    void Stab() {
        Collider[] hits = Physics.OverlapSphere(transform.position, 2f, enemyHitMask);

        foreach(var hit in hits) {
            Hit(hit, 25);
        }

        attackTime = AttackCooldown;
    }

    [ServerCallback]
    public void Hit(Collider col, int damage) {
        Damageable dmgable = col.gameObject.GetComponent<Damageable>();

        if (dmgable)
            dmgable.Damage(damage);
    }
}
