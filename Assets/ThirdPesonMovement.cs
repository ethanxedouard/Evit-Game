using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 playerVelocity;
    public bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float crouchSpeed = 1.0f; // Speed when crouching
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private bool isCrouching = false; // Track crouching state

    private float standingHeight = 2.0f; // Normal height
    private float crouchingHeight = 1.0f; // Reduced height when crouching

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (Input.GetKeyDown(KeyCode.C)) // Toggle crouch on C key
        {
            isCrouching = !isCrouching;
            controller.height = isCrouching ? crouchingHeight : standingHeight;
        }

        float currentSpeed = isCrouching ? crouchSpeed : playerSpeed;

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * currentSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer && !isCrouching) // Prevent jumping while crouching
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
