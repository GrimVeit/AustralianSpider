using System;

public class CardGameWalletModel
{
    public int Money { get; private set; }
    public event Action OnMoneySuccesTransitToBank;
    public event Action<int> OnChangeMoney;
    public event Action<int> OnAddMoney;
    public event Action<int> OnRemoveMoney;

    private IMoneyProvider moneyProvider;
    private ISoundProvider soundProvider;

    private int bet;
    private int multiply;

    public CardGameWalletModel(IMoneyProvider moneyProvider, ISoundProvider soundProvider)
    {
        this.moneyProvider = moneyProvider;
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {
        Money = 0;
        OnChangeMoney?.Invoke(Money);
    }

    public void Dispose()
    {

    }

    public void SetBet(int bet)
    {
        this.bet = bet;
    }

    public void IncreseMoney()
    {
        if(Money == 0)
        {
            Money = bet;
            OnAddMoney?.Invoke(Money);
            OnChangeMoney?.Invoke(Money);
            return;
        }

        multiply = Money * bet;
        OnAddMoney?.Invoke(multiply);

        Money += multiply;
        OnChangeMoney?.Invoke(Money);
    }

    public void DecreaseMoney()
    {
        OnRemoveMoney?.Invoke(Money);

        Money = 0;
        OnChangeMoney?.Invoke(Money);
    }

    public void TransitMoneyToBank()
    {
        soundProvider.PlayOneShot("Success");

        moneyProvider.SendMoney(Money);

        Money = 0;
        OnChangeMoney?.Invoke(Money);

        OnMoneySuccesTransitToBank?.Invoke();
    }
}
