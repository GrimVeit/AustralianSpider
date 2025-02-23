using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreCardPresenter
{
    private readonly StoreCardModel model;
    private readonly StoreCardView view;

    public StoreCardPresenter(StoreCardModel model, StoreCardView view)
    {
        this.model = model;
        this.view = view;

        ActivateEvents();
    }

    public void Initialize()
    {
        view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        view.Dispose();
    }

    private void ActivateEvents()
    {
        model.OnSetCoverCardDesign += view.SetCoverCardDesign;
        model.OnSetFaceCardDesign += view.SetFaceCardDesign;
        model.OnSetGameType += view.SetGameType;
        model.OnCreateCards += view.GenerateCards;
        model.OnDealCards += view.DealCards;
        model.OnDestroyCards += view.DestroyCards;
    }

    private void DeactivateEvents()
    {
        model.OnSetCoverCardDesign -= view.SetCoverCardDesign;
        model.OnSetFaceCardDesign -= view.SetFaceCardDesign;
        model.OnSetGameType -= view.SetGameType;
        model.OnCreateCards -= view.GenerateCards;
        model.OnDealCards -= view.DealCards;
        model.OnDestroyCards -= view.DestroyCards;
    }

    #region Input

    public event Action<List<CardInteractive>> OnDealCards_Value
    {
        add { view.OnDealCards_Value += value; }
        remove { view.OnDealCards_Value -= value; }
    }

    public event Action<List<CardInteractive>> OnDealCardsFromStock_Value
    {
        add { view.OnDealCardsFromStock_Value += value; }
        remove { view.OnDealCardsFromStock_Value -= value; }
    }

    public event Action OnDealCardsFromStock
    {
        add { view.OnDealCardsFromStock += value; }
        remove { view.OnDealCardsFromStock -= value; }
    }

    public void SetCoverCardDesign(CoverCardDesign coverCardDesign)
    {
        model.SetCoverCardDesign(coverCardDesign);
    }

    public void SetFaceCardDesign(FaceCardDesign faceCardDesign)
    {
        model.SetFaceCardDesign(faceCardDesign);
    }

    public void SetGameType(GameType gameType)
    {
        model.SetGameType(gameType);
    }

    public void DestroyCards(List<CardInteractive> cards)
    {
        model.DestroyCards(cards);
    }

    public void CreateCards()
    {
        model.CreateCards();
    }

    public void DealCards()
    {
        model.DealCards();
    }

    public void DealCardsFromStock()
    {

    }

    #endregion
}
