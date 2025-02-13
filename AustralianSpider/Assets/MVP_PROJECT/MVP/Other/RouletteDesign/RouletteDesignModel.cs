using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteDesignModel
{
    public event Action<int> OnChooseIndexDesign;

    private int currentIndex;

    public void Initialize()
    {
        currentIndex = PlayerPrefs.GetInt(PlayerPrefsKeys.ROULETTE_COLOR_INDEX, 0);
        OnChooseIndexDesign?.Invoke(currentIndex);
    }

    public void Dispose()
    {

    }
}
