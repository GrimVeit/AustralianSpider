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
        columns.ForEach(column =>
        {
            column.OnFullComplectCards += HandleFullCompleteLevelGroup;
            column.OnFullComplectCards_Value += HandleFullCompleteLevelGroup;
        });
    }

    public void Dispose()
    {
        columns.ForEach(column =>
        {
            column.OnFullComplectCards -= HandleFullCompleteLevelGroup;
            column.OnFullComplectCards_Value -= HandleFullCompleteLevelGroup;
        });
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
            columns[i].CheckFinishedSequence();
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

    public void SortDropedCard(PointerEventData pointerEventData, CardInteractive cardInteractive)
    {
        if (pointerEventData.pointerEnter != null)
        {
            Debug.Log(pointerEventData.pointerEnter.gameObject.name);

            if (pointerEventData.pointerEnter.TryGetComponent(out Column cardColumn))
            {
                if (cardColumn.CanBeDroped(cardInteractive))
                {
                    var element = cardInteractive.ParentColumn.Cards.IndexOf(cardInteractive) - 1;
                    if(element != -1)
                    {
                        OnCardDrop_Value?.Invoke(cardInteractive, cardInteractive.ParentColumn, cardInteractive.ParentColumn.Cards[element].Fliped);
                    }
                    else
                    {
                        OnCardDrop_Value?.Invoke(cardInteractive, cardInteractive.ParentColumn, false);
                    }

                    if (cardInteractive.Children != null)
                        cardInteractive.ParentColumn.RemoveCards(cardInteractive.Children);
                    cardInteractive.ParentColumn.RemoveCard(cardInteractive);
                    cardInteractive.ParentColumn.RefreshPickable();

                    cardInteractive.SetParentColumn(cardColumn);

                    cardInteractive.MoveTo(cardColumn.NewCardPosition, 0.1f, ()=> Test(cardInteractive, cardColumn));

                }
                else
                {
                    cardInteractive.ReturnToOriginalPosition();
                }
            }
            else
            {
                cardInteractive.ReturnToOriginalPosition();
            }
        }
        else
        {
            cardInteractive.ReturnToOriginalPosition();
        }
    }

    private void Test(CardInteractive card, Column cardColumn)
    {
        card.ParentColumn.RefreshPickable();

        cardColumn.AddCard(card);

        if (card.Children != null)
            cardColumn.AddCards(card.Children);

        cardColumn.RefreshPickable();
        cardColumn.CheckFinishedSequence();

        OnCardDrop?.Invoke();
    }

    public void ReturnLastMotion(CardInteractive cardInteractive, List<CardInteractive> childrens, Column column, bool isActiveHigherCard)
    {
        cardInteractive.CleanChildrens();
        cardInteractive.SetChildrens(childrens);

        //if (cardInteractive.Children != null)
        //    cardInteractive.ParentColumn.RemoveCards(cardInteractive.Children);

        //cardInteractive.ParentColumn.RemoveCard(cardInteractive);
        //cardInteractive.ParentColumn.RefreshPickable();

        //column.AddCard(cardInteractive);

        //if (cardInteractive.Children != null)
        //    column.AddCards(cardInteractive.Children);

        //column.RefreshPickable();
        //column.CheckFinishedSequence();

        if (cardInteractive.Children != null)
            cardInteractive.ParentColumn.RemoveCards(cardInteractive.Children);
        cardInteractive.ParentColumn.RemoveCard(cardInteractive);
       cardInteractive.ParentColumn.RefreshPickable();

        cardInteractive.SetParentColumn(column);

        cardInteractive.MoveTo(column.NewCardPosition, 0.1f, () => ReturnEndMove(cardInteractive, column, isActiveHigherCard));

        OnCardDrop?.Invoke();
    }

    private void ReturnEndMove(CardInteractive card, Column cardColumn, bool isActiveHigherCard)
    {
        card.ParentColumn.RefreshPickable();

        var element = cardColumn.Cards[cardColumn.Cards.Count - 1];
        if (element != null)
        {
            if (isActiveHigherCard)
            {
                cardColumn.Cards[cardColumn.Cards.Count - 1].Fliped = true;
            }
            else
            {
                cardColumn.Cards[cardColumn.Cards.Count - 1].Fliped = false;
            }
        }

        cardColumn.AddCard(card);

        if (card.Children != null)
            cardColumn.AddCards(card.Children);

        cardColumn.RefreshPickable();
        cardColumn.CheckFinishedSequence();

        OnCardDrop?.Invoke();
    }

    #region Input

    public event Action OnFullComplectCards;
    public event Action<List<CardInteractive>> OnFullComplectCards_Value;
    public event Action OnCardDrop;
    public event Action<CardInteractive, Column, bool> OnCardDrop_Value;

    private void HandleFullCompleteLevelGroup()
    {
        OnFullComplectCards?.Invoke();
    }

    private void HandleFullCompleteLevelGroup(List<CardInteractive> cardInteractives)
    {
        OnFullComplectCards_Value?.Invoke(cardInteractives);
    }

    #endregion
}
