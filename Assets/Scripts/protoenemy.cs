using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class protoenemy : MonoBehaviour
{
    public float AttackSpeed = 2f;
    public float AttackSpeed2 = 3f;
    public Transform ProjectileSpawn;
    public GameObject Projectile;
    public GameObject FireObject;

    public Transform target; // make this determined in code

    Damageable dmgable;
    float attackTime;
    bool secondPhase = false;

    void Start()
    {
        dmgable = GetComponent<Damageable>();
        attackTime = AttackSpeed; // so it doesnt attack right away
    }

    void Update()
    {
        if (GameManager.Instance.GameOver)
            return;

        if (dmgable.health <= 0)
        { 
            // dead
            return;
        }

        if (dmgable.health <= dmgable.maxHealth / 2)
        {
            secondPhase = true;
        }

        attackTime -= Time.deltaTime;

        if (attackTime <= 0)
        {
            float rnd = Random.value;

            if (secondPhase && rnd < 0.5f)
            {
                Vector3 pos = target.position;

                if (Physics.Raycast(pos, -Vector3.up, out RaycastHit hit))
                {
                    Destroy(Instantiate(FireObject, hit.point, Quaternion.identity), 6f);
                    attackTime = AttackSpeed2;
                }
            }
            else
            {
                Instantiate(Projectile, ProjectileSpawn.position, ProjectileSpawn.rotation);
                attackTime = AttackSpeed;
            }
        }
    }
}
