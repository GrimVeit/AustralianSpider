using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreCardModel
{
    private GameType currentGameType;
    private CoverCardDesign currentCoverCardDesign;
    private FaceCardDesign currentFaceCardDesign;

    public void SetGameType(GameType gameType)
    {
        currentGameType = gameType;

        OnSetGameType?.Invoke(currentGameType);
    }

    public void SetCoverCardDesign(CoverCardDesign cardDesign)
    {
        currentCoverCardDesign = cardDesign;

        OnSetCoverCardDesign?.Invoke(currentCoverCardDesign);
    }

    public void SetFaceCardDesign(FaceCardDesign faceCardDesign)
    {
        currentFaceCardDesign = faceCardDesign;

        OnSetFaceCardDesign?.Invoke(currentFaceCardDesign);
    }

    public void CreateCards()
    {
        OnCreateCards?.Invoke();
    }

    public void DealCards()
    {
        OnDealCards?.Invoke();
    }

    internal void DestroyCards(List<CardInteractive> cards)
    {
        OnDestroyCards?.Invoke(cards);
    }

    #region Input

    public event Action<GameType> OnSetGameType;
    public event Action<CoverCardDesign> OnSetCoverCardDesign;
    public event Action<FaceCardDesign> OnSetFaceCardDesign;

    public event Action<List<CardInteractive>> OnDestroyCards;
    public event Action OnCreateCards;
    public event Action OnDealCards;

    #endregion
}
