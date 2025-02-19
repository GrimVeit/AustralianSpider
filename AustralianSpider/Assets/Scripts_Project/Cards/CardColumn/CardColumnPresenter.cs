using System;
using System.Collections.Generic;

public class CardColumnPresenter
{
    private CardColumnModel model;
    private CardColumnView view;

    public CardColumnPresenter(CardColumnModel model, CardColumnView view)
    {
        this.model = model;
        this.view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        view.Dispose();
    }

    private void ActivateEvents()
    {
        model.OnDealCards += view.DealCards;
        model.OnDealCardsFromStock += view.DealCardsFromStock;

        view.OnFullCompleteLevelGroup += model.FullCompleteCardGroup;
        view.OnCardDrop += model.CardDrop;
    }

    private void DeactivateEvents()
    {
        model.OnDealCards -= view.DealCards;
        model.OnDealCardsFromStock -= view.DealCardsFromStock;

        view.OnFullCompleteLevelGroup -= model.FullCompleteCardGroup;
        view.OnCardDrop -= model.CardDrop;
    }

    #region Input

    public event Action OnFullCompleteCardGroup
    {
        add { model.OnFullCompleteCardGroup += value; }
        remove { model.OnFullCompleteCardGroup -= value; }
    }

    public event Action OnWinning
    {
        add { model.OnWinning += value; }
        remove { model.OnWinning -= value; }
    }

    public event Action OnCardDrop
    {
        add { model.OnCardDrop += value; }
        remove { model.OnCardDrop -= value; }
    }


    public void DealCards(List<CardInteractive> cardInteractives)
    {
        model.DealCards(cardInteractives);
    }

    public void DealCardsFromStock(List<CardInteractive> cardInteractives)
    {
        model.DealCardsFromStock(cardInteractives);
    }

    #endregion
}
