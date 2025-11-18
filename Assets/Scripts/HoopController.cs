using System;
using UnityEngine;

public class HoopController : MonoBehaviour
{
    public enum Team { Team1, Team2 }

    public Team pointsAwardedTo;

    public BoxCollider2D rimLineSensor;
    public BoxCollider2D netSensor;

    public Rigidbody2D ball;
    
    public float minDownwardVelocity = -0.05f;

    public ScoreController scoreController;

    private bool crossedDown;

    private void FixedUpdate()
    {
        if (!ball) return;

        Vector2 ballPosition = ball.position;
        float ballYVelocity = ball.linearVelocity.y;

        if (rimLineSensor && rimLineSensor.OverlapPoint(ballPosition))
        {
            crossedDown = true;
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
    
    
}
