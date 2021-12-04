using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerMain player;

    [Space]

    public Rigidbody rigidBody;
    public CapsuleCollider playerCollider;

    [Header("Settings")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpHeight = 1.5f;
    public float slidingStartSpeed = 15f;
    public float slidingBrakeSpeed = 5f;
    public float slidingPosOffset = 0.5f;

    [Header("Ground Check")]
    public Transform groundCheckPoint;
    public float groundCheckDistance = 0.2f;
    public LayerMask groundCheckMask;

    public bool isRunning = false;
    public bool isGrounded = false;

    private Vector3 moveInput;
    private bool jump = false;

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheckPoint.position, groundCheckDistance, groundCheckMask);

        moveInput = (transform.right * Input.GetAxisRaw("Horizontal") + transform.up * 0f + transform.forward * Input.GetAxisRaw("Vertical")).normalized;

        isRunning = moveInput.sqrMagnitude > 0;

        player.animationModule.isRunning = isRunning;

        float speedMultiplier = Input.GetKey(KeyCode.LeftShift) ? 2f : 1.5f;
        player.animationModule.SetValue("RunningSpeedMultiplier", speedMultiplier);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            jump = true;
    }

    private void FixedUpdate()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        Vector3 velo = rigidBody.velocity;
        velo.x = moveInput.x * speed;
        velo.z = moveInput.z * speed;
        rigidBody.velocity = velo;

        if (jump)
        {
            float jumpForce = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
            rigidBody.velocity += Vector3.up * jumpForce;

            jump = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckDistance);
    }
}
