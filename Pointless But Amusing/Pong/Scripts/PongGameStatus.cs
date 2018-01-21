using UnityEngine;

[RequireComponent(typeof(PongGame))]
public class PongGameStatus : MonoBehaviour
{
    public int score = 0;
    public int ball = 3;
    public int ballCount = 0;
    public int tmpBallCount = 0;
    public bool isGameover = false;
    public bool isRestarting = false;
    public int ticks = 0;

    public void AddScore()
    {
        score += ballCount;
    }

    public void Miss()
    {
        ball--;
        if (ball <= 0)
        {
            isGameover = true;
        }
    }

    public void Restart()
    {
        isRestarting = true;
        isGameover = false;
        score = 0;
        ball = 3;
        ballCount = tmpBallCount = 0;
        ticks = 0;
    }
}
