using System;
using UnityEngine;

public class HoopController : MonoBehaviour
{
    public enum Team { Team1, Team2 }

    public Team pointsAwardedTo;

    public BoxCollider2D rimLineSensor;
    public BoxCollider2D netSensor;
    public Rigidbody2D ball;
    public ScoreController scoreController;
    
    public float minDownwardVelocity = -0.05f;

    private bool crossedDown;

    /*
    private void FixedUpdate()
    {
        if (!ball) return;

        Vector2 ballPosition = ball.position;
        float ballYVelocity = ball.linearVelocity.y;

        if (rimLineSensor && rimLineSensor.OverlapPoint(ballPosition))
        {
            crossedDown = true;
            Debug.Log("Crossed Rim");
        }

        if (crossedDown && netSensor && netSensor.OverlapPoint(ballPosition))
        {
            crossedDown = false;
            Debug.Log(pointsAwardedTo + " Scored");
            //scoreController.AddPoint(pointsAwardedTo);
        }

        
        if (crossedDown && rimLineSensor && ballPosition.y > rimLineSensor.bounds.max.y + 0.1f && ballYVelocity >= 0f)
        {
            crossedDown = false;
        }
        
    }
    */
    public void OnRimEnter(Collider2D obj)
    {
        var rb = obj.attachedRigidbody;
        if (rb != ball) return;
        
        if (ball.linearVelocity.y < minDownwardVelocity)
        {
            crossedDown = true;
            Debug.Log("Crossed Rim");
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
            Debug.Log(pointsAwardedTo + " Scored");
            //scoreController.AddPoint(pointsAwardedTo);
            crossedDown = false;
        }
    }
}
