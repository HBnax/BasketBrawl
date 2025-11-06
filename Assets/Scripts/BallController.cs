using System;
using NUnit.Framework;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public LayerMask playerLayer;
    public float pickupCooldown = 0.05f;

    private bool IsHeld;
    private PlayerController Holder;

    private Rigidbody2D rb;
    private Collider2D col;
    private float lastReleaseTime = 999f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void FixedUpdate()
    {
        if (IsHeld && Holder && Holder.holdPoint)
        {
            rb.position = Holder.holdPoint.position;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (IsHeld) return;
        
        if (((1 << c.collider.gameObject.layer) & playerLayer) == 0) return;

        Debug.Log("Collided");
        
        var player = c.collider.GetComponentInParent<PlayerController>();
        if (player == null || player.hasBall || player.holdPoint == null) return;

        bool ballPickedUp = CanPickup(player);
        if (ballPickedUp)
        {
            Debug.Log("Picked Up");
        }
    }

    bool CanPickup(PlayerController player)
    {
        Debug.Log("Checking Pickup");
        if (IsHeld || player == null || player.hasBall) return false;

        IsHeld = true;
        Holder = player;
        Holder.hasBall = true;

        rb.simulated = false;
        col.enabled = false;
        
        transform.position = Holder.holdPoint.position;
        return true;
    }

    public void ReleaseBall(Vector2 impulse)
    {
        if (!IsHeld) return;
        
        IsHeld = false;
        if (Holder) Holder.hasBall = false;
        Holder = null;
        lastReleaseTime = Time.time;

        rb.simulated = true;
        col.enabled = true;

        if (impulse != Vector2.zero)
        {
            rb.AddForce(impulse, ForceMode2D.Impulse);
        }
    }

    public bool GetIsHeld()
    {
        return IsHeld;
    }
    
    public PlayerController GetHolder()
    {
        return Holder;
    }
}
