using System;

public class CardBetModel
{
    public event Action OnUpNormalBet;
    public event Action OnDownNormalBet;

    public event Action OnSubmitBet;
    public event Action<int> OnSubmitBet_Value;
    public event Action OnActivate;
    public event Action OnDeactivate;

    public event Action<int> OnChangedBet;

    private BetAmounts betAmounts;

    private int bet;
    private int currentBetIndex = 0;

    private bool isSubmitedBet = false;
    private bool isFirst = true;

    private ITutorialProvider tutorialProvider;
    private ISoundProvider soundProvider;

    public CardBetModel(BetAmounts betAmounts, ITutorialProvider tutorialProvider, ISoundProvider soundProvider)
    {
        this.betAmounts = betAmounts;
        this.tutorialProvider = tutorialProvider;
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {
        currentBetIndex = 0; 
        bet = betAmounts.betValues[currentBetIndex];

        DecreaseBet();
    }

    public void Dispose()
    {

    }

    public void Activate()
    {
        OnActivate?.Invoke();

        if (IsBetActivated())
            SubmitBet();

        if (tutorialProvider.IsActiveTutorial())
            tutorialProvider.ActivateTutorial("ChooseMoneyBet");
    }

    public void Deactivate()
    {
        OnDeactivate?.Invoke();

        if (tutorialProvider.IsActiveTutorial())
            tutorialProvider.DeactivateTutorial("ChooseMoneyBet");
    }

    public void IncreaseBet()
    {
        if (currentBetIndex < betAmounts.betValues.Count - 1)
        {
            currentBetIndex += 1;

            bet = betAmounts.betValues[currentBetIndex];

            soundProvider.PlayOneShot("IncreaseBet");

            OnChangedBet?.Invoke(bet);

            if (currentBetIndex == betAmounts.betValues.Count - 1)
                OnUpNormalBet?.Invoke();


            return;
        }

        OnUpNormalBet?.Invoke();
    }

    public void DecreaseBet()
    {
        if (currentBetIndex > 0)
        {
            currentBetIndex -= 1;

            bet = betAmounts.betValues[currentBetIndex];

            soundProvider.PlayOneShot("DecreaseBet");

            OnChangedBet?.Invoke(bet);

            if(currentBetIndex == 0)
                OnDownNormalBet?.Invoke();

            return;
        }

        OnDownNormalBet?.Invoke();
    }

    public void SubmitBet()
    {
        isSubmitedBet = true;

        if (IsBetActivated())
        {
            if (isFirst)
            {
                soundProvider.PlayOneShot("ClickOpen");
                isFirst = false;
            }
            OnSubmitBet_Value?.Invoke(bet);
            OnSubmitBet?.Invoke();
        }
    }

    public bool IsBetActivated()
    {
        return (bet != 0) && isSubmitedBet; 
    }
}
