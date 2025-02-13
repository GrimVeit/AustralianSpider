using System;
using System.Collections.Generic;

public class CardComparisionModel
{
    public event Action OnGetCards;
    public event Action<CardValue, CardValue> OnGetCards_Values;

    private List<CardValue> cards = new List<CardValue>();

    public event Action OnSuccessGame;
    public event Action OnLoseGame;

    private bool resultGame;
    private bool userCompareResult;

    private bool isGetAllCards;

    private ITutorialProvider tutorialProvider;
    private ISoundProvider soundProvider;

    public CardComparisionModel(ITutorialProvider tutorialProvider, ISoundProvider soundProvider)
    {
        this.tutorialProvider = tutorialProvider;
        this.soundProvider = soundProvider;
    }

    public void OnCardSpawned(CardValue cardValue)
    {
        cards.Add(cardValue);

        if(cards.Count == 2)
        {
            isGetAllCards = true;

            if (tutorialProvider.IsActiveTutorial())
                tutorialProvider.Deactivate();

            resultGame = cards[0].CardNominal < cards[1].CardNominal;

            if(resultGame == userCompareResult)
            {
                soundProvider.PlayOneShot("Success");
                OnSuccessGame?.Invoke();
            }
            else
            {
                soundProvider.PlayOneShot("Lose");
                OnLoseGame?.Invoke();
            }
        }
    }

    public void SubmitGetCards()
    {
        if (!isGetAllCards) return;

        OnGetCards_Values?.Invoke(cards[0], cards[1]);
        OnGetCards?.Invoke();

        cards.Clear();

        isGetAllCards = false;
    }

    public void UserCompare(bool result)
    {
        userCompareResult = result;
    }
}
