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
        Coroutines.Start(DealCards_Coro(cards));
    }

    public IEnumerator DealCards_Coro(List<CardInteractive> cards)
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
                var card = cards[indexCard];
                card.OnPickedCard += PickedCard;
                card.OnDroppedCard += SortDropedCard;

                card.SetParentColumn(columns[i]);
                card.MoveTo(card.ParentColumn.NewCardPosition, 0.08f, () => EndDealCardsFromStock(card, card.ParentColumn));

                indexCard += 1;

                yield return new WaitForSeconds(0.02f);
            }
        }

        for (int i = 0; i < 10; i++)
        {
            var card = cards[indexCard];
            card.OnPickedCard += PickedCard;
            card.OnDroppedCard += SortDropedCard;
            card.Fliped = true;
            card.Pickable = true;

            card.SetParentColumn(columns[i]);
            card.MoveTo(card.ParentColumn.NewCardPosition, 0.08f, () => EndDealCardsFromStock(card, card.ParentColumn));

            indexCard += 1;

            yield return new WaitForSeconds(0.02f);
        }
    }

    public void DealCardsFromStock(List<CardInteractive> cards)
    {
        Coroutines.Start(DealCardsFromStock_Coro(cards));
    }

    private IEnumerator DealCardsFromStock_Coro(List<CardInteractive> cards)
    {
        for (int i = 0; i < 10; i++)
        {
            var card = cards[i];
            card.OnPickedCard += PickedCard;
            card.OnDroppedCard += SortDropedCard;
            card.Fliped = true;
            card.Pickable = true;

            card.SetParentColumn(columns[i]);
            card.MoveTo(card.ParentColumn.NewCardPosition, 0.08f, () => EndDealCardsFromStock(card, card.ParentColumn));

            yield return new WaitForSeconds(0.02f);
        }
    }

    private void EndDealCardsFromStock(CardInteractive card, Column cardColumn)
    {
        cardColumn.AddCard(card);
        cardColumn.RefreshPickable();
        cardColumn.CheckFinishedSequence();
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
