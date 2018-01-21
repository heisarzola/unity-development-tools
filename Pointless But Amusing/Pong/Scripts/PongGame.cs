using System;
using System.Collections.Generic;
using UnityEngine;
using ProtoTurtle.BitmapDrawing;

[RequireComponent(typeof(PongGameStatus))]
public class PongGame : MonoBehaviour
{
    public Texture2D texture;
    public float topRacketX = 0.5f;
    public bool isTopRacketValid;
    public float bottomRacketX = 0.5f;
    public bool isBottomRacketValid;
    public int width = 240;
    public int height = 160;
    public int wallWidth = 10;
    List<Ball> balls = new List<Ball>();
    Racket topRacket;
    Racket bottomRacket;
    int missCount = 0;

    void Start()
    {
        texture = new Texture2D(width, height);
        balls.Clear();
        topRacket = new Racket(this, true);
        bottomRacket = new Racket(this, false);
        AddBall();
    }

    public void AddBall(float rankSpeed = 1)
    {
        var a = (90.0f).Rand(45);
        if ((1.0f).Rand() < 0.5f)
        {
            a += 180;
        }
        var vel = new Vector2().MoveAngle(a, 1) * rankSpeed * 0.2f;
        AddBall(new Vector2(width / 2, height / 2), vel, 0);
    }

    public void AddBall(Vector2 pos, Vector2 vel, float speed = 1)
    {
        var b = new Ball(this);
        b.pos = pos;
        b.vel = vel;
        b.speed = speed;
        balls.Add(b);
    }

    public void UpdateFrame()
    {
        if (texture == null || topRacket == null || bottomRacket == null)
        {
            Start();
        }
        texture.DrawFilledRectangle(new Rect(0, 0, width, height), Color.clear);
        var wallColor = Color.blue;
        if (missCount > 0)
        {
            wallColor = Color.red;
            missCount--;
        }
        texture.DrawFilledRectangle(new Rect(0, 0, wallWidth, height), wallColor);
        texture.DrawFilledRectangle(new Rect(width - wallWidth, 0, wallWidth, height), wallColor);
        balls = balls.FindAll((b) => b.Update());
        topRacket.x = topRacketX;
        topRacket.Update();
        isTopRacketValid = topRacket.isValid;
        bottomRacket.x = bottomRacketX;
        bottomRacket.Update();
        isBottomRacketValid = bottomRacket.isValid;
        texture.Apply();
        PongGameStatus pongGameStatus = null;
        try
        {
            pongGameStatus = gameObject.GetComponent<PongGameStatus>() as PongGameStatus;
        }
        catch (MissingReferenceException) { }
        if (pongGameStatus != null)
        {
            if (pongGameStatus.ticks % 500 == 0 && !pongGameStatus.isGameover)
            {
                AddBall(Mathf.Sqrt(1 + pongGameStatus.ticks * 0.0003f));
            }
            if (pongGameStatus.isRestarting || pongGameStatus.isGameover)
            {
                balls.Clear();
                if (pongGameStatus.isRestarting)
                {
                    AddBall();
                }
            }
            var bc = balls.Count;
            if (isTopRacketValid)
            {
                pongGameStatus.tmpBallCount = bc;
            }
            else
            {
                pongGameStatus.tmpBallCount += bc;
            }
            if (isBottomRacketValid)
            {
                pongGameStatus.ballCount = pongGameStatus.tmpBallCount;
                pongGameStatus.ticks++;
                if (pongGameStatus.isRestarting)
                {
                    pongGameStatus.isRestarting = false;
                }
            }
        }
    }

    public bool TestRacketCollision(Vector2 pos, Vector2 vel, float radius)
    {
        if (vel.y > 0)
        {
            return (bottomRacket.TestCollision(pos, radius));
        }
        else
        {
            return (topRacket.TestCollision(pos, radius));
        }
    }

    public PongGame GetAnother(int offset = 1)
    {
        try
        {
            var others = gameObject.GetComponents<PongGame>();
            var idx = Array.IndexOf(others, this) + offset;
            if (idx < 0 || idx >= others.Length)
            {
                return null;
            }
            return others[idx];
        }
        catch (MissingReferenceException)
        {
            return null;
        }
    }

    public void AddScore()
    {
        var status = gameObject.GetComponent<PongGameStatus>() as PongGameStatus;
        if (status != null)
        {
            status.AddScore();
        }
    }

    public void Miss()
    {
        missCount = 60;
        var status = gameObject.GetComponent<PongGameStatus>() as PongGameStatus;
        if (status != null)
        {
            status.Miss();
        }
    }
}
