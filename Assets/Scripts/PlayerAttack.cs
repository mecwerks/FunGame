using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform Cam;
    public GameObject Projectile;
    public Transform ProjectilePosition;
    public MeleeWeapon Weapon;
    public float attackCooldown = 0.5f;

    private Damageable dmgable;

    private float attackTime = 0;

    private void Start()
    {
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
            attackTime = attackCooldown;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Weapon.Attack();
            attackTime = attackCooldown;
        }

    }
}
