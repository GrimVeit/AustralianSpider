using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardColumn : MonoBehaviour
{
    public List<CardMove> Cards = new List<CardMove>();
    public VerticalLayoutGroup VerticalLayoutGroup;

    [SerializeField] public Transform ContentScrollView;

    public int YCardOffset = 10;

    public void RefreshRenderOrder()
    {
        for (int i = 0; i < Cards.Count; i++)
        {
            //Cards[i].SetZOrder(i);
        }
    }

    public bool CanBeDroped(CardMove card)
    {
        if (Cards.Count == 0)
            return true;

        return Cards[Cards.Count - 1].Value == card.Value + 1;
    }

    public void AddCard(CardMove card)
    {
        card.transform.SetParent(ContentScrollView);
        card.ParentColumn = this;
        Cards.Add(card);
        //card.SetZOrder(Cards.Count);
    }

    public void AddCards(List<CardMove> cards)
    {
        foreach (CardMove card in cards)
        {
            AddCard(card);
        }
    }

    public void RemoveCard(CardMove card)
    {
        Cards.Remove(card);
        if (Cards.Count > 0)
            if (!Cards[Cards.Count - 1].Fliped)
            {
                Cards[Cards.Count - 1].Fliped = true;
                Cards[Cards.Count - 1].Pickable = true;
            }
    }

    public void RemoveCards(List<CardMove> cards)
    {
        foreach (CardMove card in cards)
        {
            RemoveCard(card);
        }
    }

    public List<CardMove> GetChildrenCards(CardMove card)
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
            Cards[i].Pickable = false;

        Cards[Cards.Count - 1].Pickable = true;
        for (int i = Cards.Count - 2; i >= 0; i--)
        {
            if (Cards[i].Value == Cards[i + 1].Value + 1 && Cards[i].Fliped &&
                Cards[i].CardType == Cards[i + 1].CardType)
                Cards[i].Pickable = true;
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

                    doneCards[u].gameObject.SetActive(false);
                }

                Debug.Log("WOW");

                RemoveCards(doneCards);
                RefreshPickable();
            }
            value++;
        }
    }
}
