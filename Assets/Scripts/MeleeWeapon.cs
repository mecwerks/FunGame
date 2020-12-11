using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public int damage = 25;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    void OnTriggerEnter(Collider col)
    {
        Damageable dmgable = col.gameObject.GetComponent<Damageable>();

        if (dmgable)
            dmgable.Damage(damage);
    }
}
