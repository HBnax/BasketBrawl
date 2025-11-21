using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public LayerMask playerLayer;
    public float pickupCooldown = 0.05f;

    private bool isHeld;
    private PlayerController holder;

    private Rigidbody2D rb;
    private Collider2D col;
    private float lastReleaseTime = -999f;
    private Vector2 pendingSpawnPos;
    private bool applySpawnNextFixed;

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
        if (isHeld && holder && holder.holdPoint)
        {
            transform.position = holder.holdPoint.position;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (isHeld) return;
        if (Time.time - lastReleaseTime < pickupCooldown) return;
        if (((1 << c.collider.gameObject.layer) & playerLayer) == 0) return;
        
        var player = c.collider.GetComponentInParent<PlayerController>();
        if (player == null || player.hasBall || player.holdPoint == null) return;

        CanPickup(player);
    }

    bool CanPickup(PlayerController player)
    {
        if (isHeld || player == null || player.hasBall) return false;

        isHeld = true;
        holder = player;
        holder.hasBall = true;

        rb.simulated = false;
        col.enabled = false;
        
        transform.position = holder.holdPoint.position;
        return true;
    }

    public void ReleaseBall(Vector2 impulse)
    {
        if (!isHeld) return;
        
        isHeld = false;
        if (holder) holder.hasBall = false;
        holder = null;
        lastReleaseTime = Time.time;

        rb.simulated = true;
        col.enabled = true;

        if (impulse != Vector2.zero)
        {
            rb.AddForce(impulse, ForceMode2D.Impulse);
        }
    }

    public void ResetToSpawn(Vector2 spawnPoint)
    {
        isHeld = false;
        if (holder) holder.hasBall = false;
        holder = null;

        rb.simulated = false;
        col.enabled = false;

        pendingSpawnPos = spawnPoint;
        applySpawnNextFixed = true;
        
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.position = pendingSpawnPos;
        rb.rotation = 0f;
        rb.bodyType = RigidbodyType2D.Dynamic;
        
        StartCoroutine(ReenableNextFixed());
    }

    private IEnumerator ReenableNextFixed()
    {
        var prevInterp = rb.interpolation;
        rb.interpolation = RigidbodyInterpolation2D.None;
        
        rb.simulated = true;
        col.enabled = true;

        if (applySpawnNextFixed)
        {
            rb.position = pendingSpawnPos;
            applySpawnNextFixed = false;
        }

        yield return new WaitForFixedUpdate();
        rb.interpolation = prevInterp;
    }
    public bool GetIsHeld()
    {
        return isHeld;
    }
    
    public PlayerController GetHolder()
    {
        return holder;
    }
}
