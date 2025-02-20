using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardColumnModel
{
    public event Action OnWinning;
    public event Action OnFullCompleteCardGroup;
    public event Action OnCardDrop;

    public event Action<List<CardInteractive>> OnDealCards;
    public event Action<List<CardInteractive>> OnDealCardsFromStock;

    private int countFullCompleteGroup = 8;

    private int currentFullCompleteLevelGroup;

    public void DealCards(List<CardInteractive> cardInteractives)
    {
        OnDealCards?.Invoke(cardInteractives);
    }

    public void DealCardsFromStock(List<CardInteractive> cardInteractives)
    {
        OnDealCardsFromStock?.Invoke(cardInteractives);
    }

    public void FullCompleteCardGroup()
    {
        OnFullCompleteCardGroup?.Invoke();

        currentFullCompleteLevelGroup += 1;

        if(currentFullCompleteLevelGroup == countFullCompleteGroup)
        {
            OnWinning?.Invoke();
        }
    }

    public void CardDrop()
    {
        OnCardDrop?.Invoke();
    }
}
