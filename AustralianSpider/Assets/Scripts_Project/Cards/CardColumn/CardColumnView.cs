using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardColumnView : View
{
    [SerializeField] private List<Column> columns = new List<Column>();

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
                cards[indexCard].OnDroppedCard += SortDropedCard;
                columns[i].AddCard(cards[indexCard]);
                indexCard += 1;
            }
        }

        for (int i = 0; i < 10; i++)
        {
            cards[indexCard].OnDroppedCard += SortDropedCard;
            cards[indexCard].Fliped = true;
            cards[indexCard].Pickable = true;
            columns[i].AddCard(cards[indexCard]);
            columns[i].RefreshPickable();
            indexCard += 1;
        }
    }

    public void SortDropedCard(PointerEventData pointerEventData, CardInteractive card)
    {
        //var colX = card.transform.position.x / 1.1f;

        //int colNum = Mathf.RoundToInt(colX);

        Debug.Log("Check");

        if (pointerEventData.pointerEnter != null)
        {
            Debug.Log(pointerEventData.pointerEnter.gameObject.name);

            if (pointerEventData.pointerEnter.TryGetComponent(out Column cardColumn))
            {
                Debug.Log(pointerEventData);

                if (cardColumn.CanBeDroped(card))
                {
                    Debug.Log("Drop it");

                    if (card.Children != null)
                        card.ParentColumn.RemoveCards(card.Children);
                    card.ParentColumn.RemoveCard(card);
                    card.ParentColumn.RefreshPickable();

                    cardColumn.AddCard(card);

                    if (card.Children != null)
                        cardColumn.AddCards(card.Children);

                    cardColumn.RefreshPickable();
                    cardColumn.CheckFinishedSequence();

                }
                else
                {
                    Debug.Log("Back");

                    card.ReturnToOriginalPosition();
                }
            }
            else
            {
                Debug.Log("Back");

                card.ReturnToOriginalPosition();
            }
        }
        else
        {
            Debug.Log("Back");

            card.ReturnToOriginalPosition();
        }
    }
}
