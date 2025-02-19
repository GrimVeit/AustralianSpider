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

    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        model.OnSetCoverCardDesign += view.SetCoverCardDesign;
        model.OnSetFaceCardDesign += view.SetFaceCardDesign;
        model.OnSetGameType += view.SetGameType;

        model.OnCreateCards += view.GenerateCards;
        model.OnDealCards += view.DealCards;
    }

    private void DeactivateEvents()
    {
        model.OnSetCoverCardDesign -= view.SetCoverCardDesign;
        model.OnSetFaceCardDesign -= view.SetFaceCardDesign;
        model.OnSetGameType -= view.SetGameType;

        model.OnCreateCards -= view.GenerateCards;
        model.OnDealCards -= view.DealCards;
    }

    #region Input

    public event Action<List<CardInteractive>> OnDealCards
    {
        add { view.OnDealCards += value; }
        remove { view.OnDealCards -= value; }
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
