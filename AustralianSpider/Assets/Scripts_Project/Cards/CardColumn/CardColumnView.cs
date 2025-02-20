using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardColumnView : View
{
    [SerializeField] private List<Column> columns = new List<Column>();
    [SerializeField] private Transform transformParent;
    private CardInteractive currentCardInteractive;

    public void Initialize()
    {
        columns.ForEach(column => column.OnFullCompleteLevel += HandleFullCompleteLevelGroup);
    }

    public void Dispose()
    {
        columns.ForEach(column => column.OnFullCompleteLevel -= HandleFullCompleteLevelGroup);
    }

    public void DealCards(List<CardInteractive> cards)
    {
        var indexCard = 0;

        for (int i = 0; i < 10; i++)
        {
            int unFlipedCards = 5;
            if (i > 3)
            {
                unFlipedCards = 4;
            }

            for (int j = 0; j < unFlipedCards; j++)
            {
                cards[indexCard].OnPickedCard += PickedCard;
                cards[indexCard].OnDroppedCard += SortDropedCard;
                columns[i].AddCard(cards[indexCard]);
                indexCard += 1;
            }
        }

        for (int i = 0; i < 10; i++)
        {
            cards[indexCard].OnPickedCard += PickedCard;
            cards[indexCard].OnDroppedCard += SortDropedCard;
            cards[indexCard].Fliped = true;
            cards[indexCard].Pickable = true;
            columns[i].AddCard(cards[indexCard]);
            columns[i].RefreshPickable();
            indexCard += 1;
        }
    }

    public void DealCardsFromStock(List<CardInteractive> cards)
    {
        for (int i = 0; i < 10; i++)
        {
            cards[i].OnPickedCard += PickedCard;
            cards[i].OnDroppedCard += SortDropedCard;
            cards[i].Fliped = true;
            cards[i].Pickable = true;
            columns[i].AddCard(cards[i]);
            columns[i].RefreshPickable();
        }
    }

    public void PickedCard(CardInteractive cardInteractive)
    {
        if(currentCardInteractive != null)
        {
            //currentCardInteractive.ReturnToOriginalPosition();
        }
        currentCardInteractive = cardInteractive;
        currentCardInteractive.transform.SetParent(transformParent);
    }

    public void SortDropedCard(PointerEventData pointerEventData, CardInteractive card)
    {
        //var colX = card.transform.position.x / 1.1f;

        //int colNum = Mathf.RoundToInt(colX);

        if (pointerEventData.pointerEnter != null)
        {
            Debug.Log(pointerEventData.pointerEnter.gameObject.name);

            if (pointerEventData.pointerEnter.TryGetComponent(out Column cardColumn))
            {
                if (cardColumn.CanBeDroped(card))
                {
                    if (card.Children != null)
                        card.ParentColumn.RemoveCards(card.Children);
                    card.ParentColumn.RemoveCard(card);
                    card.ParentColumn.RefreshPickable();

                    cardColumn.AddCard(card);

                    if (card.Children != null)
                        cardColumn.AddCards(card.Children);

                    cardColumn.RefreshPickable();
                    cardColumn.CheckFinishedSequence();

                    OnCardDrop?.Invoke();

                }
                else
                {
                    card.ReturnToOriginalPosition();
                }
            }
            else
            {
                card.ReturnToOriginalPosition();
            }
        }
        else
        {
            card.ReturnToOriginalPosition();
        }
    }

    #region Input

    public event Action OnFullCompleteLevelGroup;
    public event Action OnCardDrop;

    private void HandleFullCompleteLevelGroup()
    {
        OnFullCompleteLevelGroup?.Invoke();
    }

    #endregion
}
