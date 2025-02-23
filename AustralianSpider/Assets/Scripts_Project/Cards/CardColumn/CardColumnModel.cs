using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardColumnModel
{
    public event Action OnWinning;
    public event Action OnFullCompleteCardGroup;
    public event Action<List<CardInteractive>> OnFullCompleteCardGroup_Value;
    public event Action OnCardDrop;
    public event Action<CardInteractive, Column, bool> OnCardDrop_Value;

    public event Action<List<CardInteractive>> OnDealCards;
    public event Action<List<CardInteractive>> OnDealCardsFromStock;
    public event Action<CardInteractive, List<CardInteractive>, Column, bool> OnReturnLastMotion;

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

    public void FullCompleteCardGroup(List<CardInteractive> cardInteractives)
    {
        OnFullCompleteCardGroup_Value?.Invoke(cardInteractives);
    }

    public void CardDrop()
    {
        OnCardDrop?.Invoke();
    }

    public void CardDrop(CardInteractive cardInteractive, Column column, bool isActiveHigherCard)
    {
        OnCardDrop_Value?.Invoke(cardInteractive, column, isActiveHigherCard);
    }

    public void ReturnLastMotion(CardInteractive cardInteractive, List<CardInteractive> childrens, Column column, bool isActiveHigherCard)
    {
        OnReturnLastMotion?.Invoke(cardInteractive, childrens, column, isActiveHigherCard);
    }
}
