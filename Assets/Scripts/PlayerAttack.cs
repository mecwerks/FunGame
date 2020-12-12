using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
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
        movement = GetComponent<PlayerMovement>();
        dmgable = GetComponent<Damageable>();
    }

    private void Update()
    {
        attackTime -= Time.deltaTime;

        if (GameManager.Instance.GameOver)
            return;

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
            // need to make this go towards where the camera is pointing
            Instantiate(Projectile, ProjectilePosition.position, ProjectilePosition.rotation);
            attackTime = AttackCooldown;
            movement.StrafeMode();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Weapon.Attack();
            attackTime = AttackCooldown;
        }

    }
}
