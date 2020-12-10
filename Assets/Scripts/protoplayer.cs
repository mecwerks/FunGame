using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class protoplayer : MonoBehaviour
{
    public float Speed = 10f;
    public float JumpVelocity = 8f;
    public float Gravity = 20f;
    public float TurnSpeed = 4f;
    public float MinPitch = -30f;
    public float MaxPitch = 60f;

    public Transform CamHolder;
    public Transform ProjectileSpawn;

    public GameObject Projectile;

    private Damageable dmgable;
    private CharacterController characterController;
    private Vector3 velocity = Vector3.zero;
    private float pitch = 0f;

    void Start()
    {
        dmgable = GetComponent<Damageable>();
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;

        // check if controller is valid
        // check if camera is valid
    }

    void Update()
    {
        if (GameManager.Instance.GameOver)
            return;

        if (dmgable.health <= 0)
        {
            return;
        }

        if (characterController.isGrounded == true)
        {
            velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            velocity = transform.TransformDirection(velocity);
            velocity *= Speed;

            if (Input.GetKey(KeyCode.Space))
            {
                velocity.y = JumpVelocity;
            }
        }

        velocity.y -= Gravity * Time.deltaTime; // apply gravity
        characterController.Move(velocity * Time.deltaTime); // move the player
        pitch -= Input.GetAxisRaw("Mouse Y")* TurnSpeed; // adjust pitch
        pitch = Mathf.Clamp(pitch, MinPitch, MaxPitch); // clamp vertical rotation
        CamHolder.eulerAngles = new Vector3(pitch, CamHolder.eulerAngles.y, CamHolder.eulerAngles.z); // set rotation on cam holder
        transform.Rotate(new Vector3(0, Input.GetAxisRaw("Mouse X"), 0) * TurnSpeed); // rotate the player horizontally

        // firingz
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // check if projectile is valid
            Instantiate(Projectile, ProjectileSpawn.position, ProjectileSpawn.rotation);
        }
    }
}
