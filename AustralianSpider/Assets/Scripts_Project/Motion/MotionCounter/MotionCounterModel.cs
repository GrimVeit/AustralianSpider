using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionCounterModel
{
    public event Action<int> OnChangeMotionCount;

    private int currentCountMotioon;

    private IMoneyProvider moneyProvider;
    private ISoundProvider soundProvider;

    public MotionCounterModel(IMoneyProvider moneyProvider, ISoundProvider soundProvider)
    {
        this.moneyProvider = moneyProvider;
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {
        currentCountMotioon = 0;
        OnChangeMotionCount?.Invoke(currentCountMotioon);
    }


    public void Dispose()
    {

    }

    public void AddMotion()
    {
        currentCountMotioon += 1;
        OnChangeMotionCount?.Invoke(currentCountMotioon);
    }

    public void RemoveScore(int score)
    {
        currentCountMotioon -= score;
        OnChangeMotionCount?.Invoke(currentCountMotioon);
    }
}
