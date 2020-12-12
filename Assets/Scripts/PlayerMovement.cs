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

    CharacterController controller;
    Vector3 velocity = Vector3.zero;
    float turnSmoothVelocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(inputX, 0f, inputY);
        float yVel = velocity.y;

        if (moveDir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, TurnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f); // use smoothed angle  here

            velocity = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            velocity *= Speed;
        }
        else
        {
            velocity = Vector3.zero;
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
            velocity.y = yVel; // preserve y velocity if in the air
        }

        velocity.y -= Gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}