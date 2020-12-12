using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform Cam;
    public float Speed = 10f;
    public float JumpVelocity = 8f;
    public float Gravity = 20f;
    public float TurnSmoothTime = 0.1f;
    public float StrafeAimTime = 1f; // time after attacking to exit strafe movement mode

    CharacterController controller;
    Vector3 velocity = Vector3.zero;
    float strafeAimTimer = 0;
    float turnSmoothVelocity; // for tracking turning dampening

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (strafeAimTimer > 0)
            strafeAimTimer -= Time.deltaTime;

        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        float yVel = velocity.y; // save vertical velocity for later

        if (moveDir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;

            if (strafeAimTimer <= 0)
            {
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, TurnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }

            velocity = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            velocity *= Speed;
        }
        else
        {
            velocity = Vector3.zero;
        }

        // snap rotation to cam dir if in strafe mode
        if (strafeAimTimer > 0)
        {
            transform.rotation = Quaternion.Euler(0f, Cam.eulerAngles.y, 0f);
        }

        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                velocity.y = JumpVelocity;
            }
        }
        else
        {
            velocity.y = yVel; // reset vertical vel
        }

        velocity.y -= Gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void StrafeMode()
    {
        strafeAimTimer = StrafeAimTime;
    }
}