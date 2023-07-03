using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;

    [Header("Movement")]
    public float gravity = -9.81f;
    public float speed = 5f;
    public float jumpHeight = 3f;

    private float crouchTimer = 1f;
    private bool crouching;
    private bool lerpCrouch;

    private bool sprinting;

    private bool isGrounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isGrounded = controller.isGrounded;

        crouchTimer += Time.deltaTime;
        float p = crouchTimer / 1f;
        p *= p;
        if (crouching)
            controller.height = Mathf.Lerp(controller.height, 1f, p);
        else
            controller.height = Mathf.Lerp(controller.height, 2f, p);

        if (p > 1f)
        {
            lerpCrouch = false;
            crouchTimer = 1f;
        }
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        playerVelocity.y += gravity * Time.deltaTime;

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        controller.Move(playerVelocity * Time.deltaTime);

    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        lerpCrouch = true;
        crouchTimer = 0f;
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting)
            speed = 8f;
        else
            speed = 5f;
    }

}
