using System;

public class CardComparisionPresenter
{
    private CardComparisionModel cardComparisionModel;

    public CardComparisionPresenter(CardComparisionModel cardComparisionModel)
    {
        this.cardComparisionModel = cardComparisionModel;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    #region Input

    public void OnSpawnedCard(CardValue cardValue)
    {
        cardComparisionModel.OnCardSpawned(cardValue);
    }

    public void SubmitGetCards()
    {
        cardComparisionModel.SubmitGetCards();
    }

    public void UserCompare(bool compare)
    {
        cardComparisionModel.UserCompare(compare);
    }

    public event Action OnSuccessGame
    {
        add { cardComparisionModel.OnSuccessGame += value; }
        remove { cardComparisionModel.OnSuccessGame -= value; }
    }

    public event Action OnLoseGame
    {
        add { cardComparisionModel.OnLoseGame += value; }
        remove { cardComparisionModel.OnLoseGame -= value; }
    }

    public event Action OnGetCards
    {
        add { cardComparisionModel.OnGetCards += value; }
        remove { cardComparisionModel.OnGetCards -= value; }
    }

    public event Action<CardValue, CardValue> OnGetCards_Values
    {
        add { cardComparisionModel.OnGetCards_Values += value; }
        remove { cardComparisionModel.OnGetCards_Values -= value; }
    }

    #endregion
}
