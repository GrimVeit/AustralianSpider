using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardColumnModel
{
    public event Action<List<CardInteractive>> OnDealCards;

    public void DealCards(List<CardInteractive> cardInteractives)
    {
        OnDealCards?.Invoke(cardInteractives);
    }
}
