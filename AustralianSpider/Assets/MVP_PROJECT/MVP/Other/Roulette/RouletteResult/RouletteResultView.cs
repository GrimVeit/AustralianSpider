using System;
using UnityEngine;

public class RouletteResultView : View
{
    [SerializeField] private Transform transformDisplay;
    [SerializeField] private Transform transformExit;
    [SerializeField] private RouletteDisplayResult displayResult;

    public void Initialize()
    {
        displayResult.Initialize();
    }

    public void Dispose()
    {
        displayResult.Dispose();
    }

    public void ShowResult(RouletteSlotValue rouletteSlotValue)
    {
        displayResult.SetData(rouletteSlotValue.RouletteNumber.Sprite);
        displayResult.Show(rouletteSlotValue.SlotTransform.position, transformDisplay.position);
    }

    public void HideResult()
    {
        displayResult.Hide(transformExit.position);
    }

    #region Input

    public event Action OnStartShowResult
    {
        add { displayResult.OnStartShowResult += value; }
        remove { displayResult.OnStartShowResult -= value; }
    }

    public event Action OnFinishShowResult
    {
        add { displayResult.OnFinishShowResult += value; }
        remove { displayResult.OnFinishShowResult -= value; }
    }

    public event Action OnStartHideResult
    {
        add { displayResult.OnStartHideResult += value; }
        remove { displayResult.OnStartHideResult -= value; }
    }

    public event Action OnFinishHideResult
    {
        add { displayResult.OnFinishHideResult += value; }
        remove { displayResult.OnFinishHideResult -= value; }
    }

    #endregion
}
