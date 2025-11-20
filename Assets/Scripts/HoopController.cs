using System;
using UnityEngine;

public class HoopController : MonoBehaviour
{
    public ScoreController scoreController;
    public ScoreController.Team pointsAwardedTo;
    
    public BoxCollider2D rimLineSensor;
    public BoxCollider2D netSensor;
    public Rigidbody2D ball;
    
    
    public float minDownwardVelocity = -0.05f;

    private bool crossedDown;

    public void OnRimEnter(Collider2D obj)
    {
        var rb = obj.attachedRigidbody;
        if (rb != ball) return;
        
        if (ball.linearVelocity.y < minDownwardVelocity)
        {
            crossedDown = true;
        }
    }
    
    public void OnRimExit(Collider2D obj)
    {
        var rb = obj.attachedRigidbody;
        if (rb != ball) return;

        if (ball.linearVelocity.y >= 0f)
            crossedDown = false;
    }

    public void OnNetEnter(Collider2D obj)
    {
        var rb = obj.attachedRigidbody;
        if (rb != ball) return;

        if (crossedDown)
        {
            scoreController.AddPoint(pointsAwardedTo);
            crossedDown = false;
        }
    }
}
