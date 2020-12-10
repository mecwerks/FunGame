using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePillar : MonoBehaviour
{
    public ParticleSystem psystem;
    public Collider pcollider;

    float timer = 1f;

    void Update()
    {
        if (timer <= 0)
            return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            psystem.Play();
            pcollider.enabled = true;
        }
    }
}
