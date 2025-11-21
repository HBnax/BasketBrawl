using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float jumpForce = 8f;

    public Transform groundCheck;
    public float groundCheckRadius = 0.08f;

    private int jumpsLeft;

    public Transform holdPoint;
    public float holdPointOffsetX = 2f;
    private float currDirection = 1f;
    public bool hasBall;
    public float throwSpeed = 7f;
    public float throwUpwardForce = 3f;
    
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
        if (jumpRequested)
        {
            DoJump();
        }
        
        jumpRequested = false;
        
        UpdateDirection();
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

    public void OnShoot(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && hasBall){
            float dirX = sr.flipX ? -1f : 1f;
            Vector2 impulse = new Vector2(dirX * throwSpeed, throwUpwardForce);
            
            var ball = FindFirstObjectByType<BallController>();
            if (ball || ball.GetIsHeld() || ball.GetHolder() == this)
            {
                ball.ReleaseBall(impulse);
            }
        }
    }

    void UpdateDirection()
    {
        if (moveX > 0.01f)
        {
            sr.flipX = false;
            currDirection = 1f;
        }
        else if (moveX < -0.01f)
        {
            sr.flipX = true;
            currDirection = -1f;
        }

        SetHoldPoint(currDirection);
    }

    void SetHoldPoint(float direction)
    {
        holdPoint.localPosition = new Vector2(holdPointOffsetX * direction, -1);
    }

    private void OnDrawGizmosSelected()
    {
        if (!groundCheck) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
