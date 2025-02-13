using System;
using UnityEngine;

public class GameSessionModel : MonoBehaviour
{
    public event Action OnWinFirstUser;
    public event Action OnWinSecondUser;

    public event Action OnChangedToSecondUser;
    public event Action OnChangedToFirstUser;

    private int currentScore = 0;

    private int firstScore;
    private int secondScore;

    public void ChangeToFirstUser()
    {
        OnChangedToFirstUser?.Invoke();
    }

    public void ChangeToSecondUser()
    {
        OnChangedToSecondUser?.Invoke();
    }

    public void SetScore(int score)
    {
        Debug.Log(score);

        currentScore += 1;

        if (currentScore == 1)
        {
            firstScore = score;
        }
        else if(currentScore == 2)
        {
            secondScore = score;

            if(firstScore >= secondScore)
            {
                OnWinFirstUser?.Invoke();
            }
            else
            {
                OnWinSecondUser?.Invoke();
            }
        }
    }
}
