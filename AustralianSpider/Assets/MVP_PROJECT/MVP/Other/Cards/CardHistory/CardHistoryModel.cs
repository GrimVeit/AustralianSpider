using System;

public class CardHistoryModel
{
    public event Action<CardValue, CardValue> OnAddCardCombo;
    public event Action OnClearHistory;

    public event Action OnLeftScroll;
    public event Action OnRightScroll;

    private ISoundProvider soundProvider;

    public CardHistoryModel(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void AddCardCombo(CardValue leftCard, CardValue rightCard)
    {
        soundProvider.PlayOneShot("Whoosh");
        OnAddCardCombo?.Invoke(leftCard, rightCard);
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
