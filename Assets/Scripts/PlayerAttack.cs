using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : NetworkBehaviour
{
    public Transform Cam;
    public GameObject Projectile;
    public Transform ProjectilePosition;
    public MeleeWeapon Weapon;
    public float AttackCooldown = 0.5f;

    private PlayerMovement movement;
    private Damageable dmgable;
    private float attackTime = 0;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        movement = GetComponent<PlayerMovement>();
        dmgable = GetComponent<Damageable>();
    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        attackTime -= Time.deltaTime;

        if (dmgable.health <= 0)
        {
            return;
        }

        if (attackTime > 0)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireProjectile();
            movement.StrafeMode();
            attackTime = AttackCooldown;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Stab();
            attackTime = AttackCooldown;
        }
    }

    [Command]
    void FireProjectile()
    {
        // need to make this go towards where the camera is pointing
        GameObject go = Instantiate(Projectile, ProjectilePosition.position, ProjectilePosition.rotation);
        NetworkServer.Spawn(go);
        attackTime = AttackCooldown;
    }

    [Command]
    void Stab()
    {
        Weapon.Attack();
        attackTime = AttackCooldown;
    }
}
