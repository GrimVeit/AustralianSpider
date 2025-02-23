using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Column : MonoBehaviour
{
    public List<CardInteractive> Cards = new List<CardInteractive>();
    public VerticalLayoutGroup VerticalLayoutGroup;

    [SerializeField] public Transform ContentScrollView;
    [SerializeField] private Transform transformFirstPosition;

    public int YCardOffset = 10;

    public Vector3 NewCardPosition
    {
        get
        {
            if(Cards.Count == 0)
            {
                return transformFirstPosition.position;
            }
            else
            {
                return Cards[^1].transform.position - new Vector3(0, 0.28f, 0);
            }
        }
    }

    public bool CanBeDroped(CardInteractive card)
    {
        if (Cards.Count == 0)
            return true;

        return Cards[Cards.Count - 1].Value == card.Value + 1;
    }

    public void AddCard(CardInteractive card)
    {
        card.transform.SetParent(ContentScrollView);
        card.SetParentColumn(this);
        Cards.Add(card);
        //card.SetZOrder(Cards.Count);
    }

    public void AddCards(List<CardInteractive> cards)
    {
        foreach (CardInteractive card in cards)
        {
            AddCard(card);
        }
    }

    public void RemoveCard(CardInteractive card)
    {
        Cards.Remove(card);
        if (Cards.Count > 0)
            if (!Cards[Cards.Count - 1].Fliped)
            {
                Debug.Log("Открытие новой карты - " + Cards[Cards.Count - 1].Value);
                Cards[Cards.Count - 1].Fliped = true;
                Cards[Cards.Count - 1].Pickable = true;
            }
    }

    public void RemoveCards(List<CardInteractive> cards)
    {
        foreach (CardInteractive card in cards)
        {
            RemoveCard(card);
        }
    }

    public List<CardInteractive> GetChildrenCards(CardInteractive card)
    {
        int cardIndex = Cards.IndexOf(card);

        if (cardIndex == Cards.Count - 1)
            return (null);

        cardIndex++;

        return Cards.GetRange(cardIndex, Cards.Count - cardIndex);
    }

    public void RefreshPickable()
    {
        if (Cards.Count == 0)
            return;

        for (int i = 0; i < Cards.Count; i++)
        {
            Cards[i].Pickable = false;
        }

        Cards[Cards.Count - 1].Pickable = true;
        for (int i = Cards.Count - 2; i >= 0; i--)
        {
            if (Cards[i].Value == Cards[i + 1].Value + 1 && Cards[i].Fliped &&
                Cards[i].CardType == Cards[i + 1].CardType)
            {
                if (!Cards[i].Pickable)
                {
                    Debug.Log("junkm");
                }
                Cards[i].Pickable = true;
            }
            else
                break;
        }
    }

    public void CheckFinishedSequence()
    {
        int value = 0;
        CardType cardType = Cards[Cards.Count - 1].CardType;

        //Reverse loop, becuase of card stacking
        for (int i = Cards.Count - 1; i >= 0; i--)
        {
            if (Cards[i].Value != value || Cards[i].CardType != cardType)
                break;

            if (Cards[i].Value == 12)
            {
                var doneCards = Cards.GetRange(i, (Cards.Count) - i);

                for (int u = 0; u < doneCards.Count; u++)
                {
                    //doneCards[u].Pickable = false;
                    //doneCards[u].transform.position = new Vector3(-1, 0);

                    //doneCards[u].gameObject.SetActive(false);
                }

                Debug.Log("WOW");

                RemoveCards(doneCards);
                RefreshPickable();

                OnFullComplectCards_Value?.Invoke(doneCards);
                OnFullComplectCards?.Invoke();
            }
            value++;
        }
    }

    #region Input

    public event Action OnFullComplectCards;
    public event Action<List<CardInteractive>> OnFullComplectCards_Value;

    #endregion
}
