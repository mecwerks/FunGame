using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.AI;

public class Enemy : NetworkBehaviour {
    public enum EnemyState {
        Wander,
        Dead
    }

    public float wanderRadius;
    public float wanderTimer;
    public MeshRenderer meshRend;
    public Material deadMaterial;

    private Damageable dmgable;
    private NavMeshAgent navAgent;
    private float timer;

    EnemyState currentState = EnemyState.Wander;

    private void Start() {
        if (!isServer) {
            return;
        }

        dmgable = GetComponent<Damageable>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        if (!isServer) {
            return;
        }

        if (dmgable.health <= 0) {
            currentState = EnemyState.Dead;
        } else if (timer >= 0) {
            timer -= Time.deltaTime;
        }

        switch (currentState) {
            case EnemyState.Wander:
                if (timer <= 0) {
                    timer = wanderTimer;
                    Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                    navAgent.SetDestination(newPos);
                }
                break;
            case EnemyState.Dead:
                meshRend.material = deadMaterial;
                break;
            default:
                break;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMesh.SamplePosition(randDirection, out NavMeshHit navHit, dist, layermask);

        return navHit.position;
    }
}
