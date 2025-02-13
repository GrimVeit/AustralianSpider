using System;

public class RouletteHistoryModel
{
    public event Action<RouletteNumber> OnAddRouletteNumberHistory;
    public event Action OnClearHistory;

    public event Action OnLeftScroll;
    public event Action OnRightScroll;

    private ISoundProvider soundProvider;

    public RouletteHistoryModel(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void AddRouletteNumber(RouletteSlotValue rouletteSlotValue)
    {
        //soundProvider.PlayOneShot("Whoosh");
        OnAddRouletteNumberHistory?.Invoke(rouletteSlotValue.RouletteNumber);
    }

    public void Clear()
    {
        OnClearHistory?.Invoke();
    }

    public void LeftScroll()
    {
        OnLeftScroll?.Invoke();
    }

    public void RightScroll()
    {
        OnRightScroll?.Invoke();
    }
}
