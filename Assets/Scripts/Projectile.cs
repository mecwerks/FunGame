using Mirror;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : NetworkBehaviour
{
    public float m_Speed = 10f;   // this is the projectile's speed
    public float m_Lifespan = 3f; // this is the projectile's lifespan (in seconds)
    public int damage = 10;

    private Rigidbody m_Rigidbody;

    void Start()
    {
        if (!isServer)
        {
            return;
        }
         m_Rigidbody = GetComponent<Rigidbody>();
         
        m_Rigidbody.AddForce(m_Rigidbody.transform.forward * m_Speed);
        Destroy(gameObject, m_Lifespan);
    }

    [ServerCallback]
    void OnTriggerEnter(Collider col)
    {
        Damageable dmgable = col.gameObject.GetComponent<Damageable>();

        if (dmgable)
            dmgable.Damage(damage);

        Destroy(gameObject);
    }
}
