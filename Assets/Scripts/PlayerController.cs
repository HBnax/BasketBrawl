using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float jumpForce = 8f;

    public Transform groundCheck;
    public Transform headCheck;
    public float groundCheckRadius = 0.08f;
    public LayerMask groundLayer;

    public int maxJumps = 1;
    private int jumpsLeft;
    
    
    Rigidbody2D rb;
    SpriteRenderer sr;
    float moveX;
    bool jumpRequested;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

        //bool isGrounded = true;
        bool isGrounded = (groundCheck || headCheck)&& Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded) jumpsLeft = maxJumps;
        
        if (jumpRequested)
        {
            if (isGrounded) DoJump();
            else if (jumpsLeft > 0)
            {
                DoJump();
                jumpsLeft--;
            }
        }
        
        jumpRequested = false;

        if (moveX > 0.01f)
            sr.flipX = false;
        else if (moveX < -0.01f)
            sr.flipX = true;
    }

    void DoJump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();
        moveX = input.x;
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            jumpRequested = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!groundCheck) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
