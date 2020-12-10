using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public bool lockY = false;
    public Transform target;

    void Start()
    {
        transform.LookAt(getTargetPosition());
    }

    void Update()
    {
        transform.LookAt(getTargetPosition());
    }

    Vector3 getTargetPosition()
    {
        Vector3 targetPos = target.position;

        if (lockY)
            targetPos.y = transform.position.y; // lock y rotation

        return targetPos;
    }
}
