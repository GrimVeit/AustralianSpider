using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        model.OnDealCards += view.DealCards;
    }

    private void DeactivateEvents()
    {
        model.OnDealCards -= view.DealCards;
    }

    #region Input

    public void DealCards(List<CardInteractive> cardInteractives)
    {
        model.DealCards(cardInteractives);
    }

    #endregion
}
