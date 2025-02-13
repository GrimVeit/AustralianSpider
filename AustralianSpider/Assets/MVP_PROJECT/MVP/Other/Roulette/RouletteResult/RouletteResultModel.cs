using System;

public class RouletteResultModel
{
    public event Action OnStartShowResult;
    public event Action OnFinishShowResult;
    public event Action<RouletteSlotValue> OnStartHideResult;
    public event Action OnFinishHideResult;

    public event Action<RouletteSlotValue> OnShowResult;
    public event Action OnHideResult;

    private RouletteSlotValue rouletteSlotValue;

    public void ShowResult(RouletteSlotValue rouletteSlotValue)
    {
        this.rouletteSlotValue = rouletteSlotValue;
        OnShowResult?.Invoke(this.rouletteSlotValue);
    }

    public void HideResult()
    {
        OnHideResult?.Invoke();
    }

    public void StartShowResult()
    {
        OnStartShowResult?.Invoke();
    }

    public void FinishShowResult()
    {
        OnFinishShowResult?.Invoke();
    }

    public void StartHideResult()
    {
        OnStartHideResult?.Invoke(rouletteSlotValue);
    }

    public void FinishHideResult()
    {
        OnFinishHideResult?.Invoke();
    }
}
