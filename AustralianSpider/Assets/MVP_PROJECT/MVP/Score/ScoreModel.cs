using System;
using UnityEngine;

public class ScoreModel
{
    public event Action<int> OnChangeAllCountScore;

    private int currentScore;

    private IMoneyProvider moneyProvider;
    private ISoundProvider soundProvider;
     
    public ScoreModel(IMoneyProvider moneyProvider, ISoundProvider soundProvider)
    {
        this.moneyProvider = moneyProvider;
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {
        currentScore = 0;
        OnChangeAllCountScore?.Invoke(currentScore);
    }


    public void Dispose()
    {
        if(currentScore > 0)
        {
            moneyProvider.SendMoney(currentScore);
        }
    }
    
    public void AddScore(int score)
    {
        currentScore += score;
        OnChangeAllCountScore?.Invoke(currentScore);
    }

    public void RemoveScore(int score)
    {
        currentScore -= score;
        OnChangeAllCountScore?.Invoke(currentScore);
    }
}
