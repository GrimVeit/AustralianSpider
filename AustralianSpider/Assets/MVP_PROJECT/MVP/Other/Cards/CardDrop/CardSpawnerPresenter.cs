using System;

public class CardSpawnerPresenter
{
    private CardSpawnerModel cardSpawnerModel;
    private CardSpawnerView cardSpawnerView;

    public CardSpawnerPresenter(CardSpawnerModel cardSpawnerModel, CardSpawnerView cardSpawnerView)
    {
        this.cardSpawnerModel = cardSpawnerModel;
        this.cardSpawnerView = cardSpawnerView;
    }

    public void Initialize()
    {
        ActivateEvents();

        cardSpawnerView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        cardSpawnerView.Dispose();
    }

    private void ActivateEvents()
    {
        cardSpawnerView.OnSpawnCard += cardSpawnerModel.SpawnCard;

        cardSpawnerModel.OnSpawnCard_Values += cardSpawnerView.SpawnCard;
        cardSpawnerModel.OnDestroyCard += cardSpawnerView.DestroyCard;
    }

    private void DeactivateEvents()
    {
        cardSpawnerView.OnSpawnCard -= cardSpawnerModel.SpawnCard;

        cardSpawnerModel.OnSpawnCard_Values -= cardSpawnerView.SpawnCard;
        cardSpawnerModel.OnDestroyCard -= cardSpawnerView.DestroyCard;
    }

    #region Input

    public event Action<CardValue> OnSpawnCard_Values
    {
        add { cardSpawnerModel.OnSpawnCard_Values += value; }
        remove { cardSpawnerModel.OnSpawnCard_Values -= value; }
    }

    public event Action OnSpawnCard
    {
        add { cardSpawnerModel.OnSpawnCard += value; }
        remove { cardSpawnerModel.OnSpawnCard -= value; }
    }

    public void SpawnCard()
    {
        cardSpawnerModel.SpawnCard();
    }

    public void DestroyCard()
    {
        cardSpawnerModel.DestroyCard();
    }

    #endregion
}
