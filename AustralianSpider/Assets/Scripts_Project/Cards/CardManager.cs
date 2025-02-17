using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Linq;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;
    public DealSettings DealSettings;

    public Sprite Cardback;
    public CardMove CardPrefab;

    public List<CardMove> AllCards;

    public GameObject CardParent;

    public List<CardColumn> Columns = new List<CardColumn>();

    /// <summary>
    /// Cards sets must be ordered same as enum CardColors
    /// 0 - Club, 1 - Hearts ...
    /// </summary>
    public Cards hearts;
    public Cards spades;
    public Cards diamonds;
    public Cards clubs;


    //Clubs_Krest,
    //Diamonds_Bubna,
    //Heart_Cherv,
    //Spade_Peak

    public int DealtCardsIndex = 0;
    public int StockDealt = 0;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        //DealButtonText.text = "Deal Another - " + 5;

        GenerateCards(DealSettings);

        Shuffle(AllCards);
        DealCards();
    }

    private void GenerateCards(DealSettings dealSettings)
    {
        if (dealSettings == DealSettings.OneSuit)
        {
            for (int i = 0; i < 8; i++)
            {
                AllCards.AddRange(GenerateCards(CardType.Clubs_Krest));
            }
        }
        else if (dealSettings == DealSettings.TwoSuit)
        {
            for (int i = 0; i < 4; i++)
            {
                AllCards.AddRange(GenerateCards(CardType.Clubs_Krest));
            }

            for (int i = 0; i < 4; i++)
            {
                AllCards.AddRange(GenerateCards(CardType.Heart_Cherv));
            }
        }
        else if (dealSettings == DealSettings.FourSuit)
        {
            for (int i = 0; i < 2; i++)
            {
                AllCards.AddRange(GenerateCards(CardType.Clubs_Krest));
            }

            for (int i = 0; i < 2; i++)
            {
                AllCards.AddRange(GenerateCards(CardType.Heart_Cherv));
            }
            for (int i = 0; i < 2; i++)
            {
                AllCards.AddRange(GenerateCards(CardType.Diamonds_Bubna));
            }

            for (int i = 0; i < 2; i++)
            {
                AllCards.AddRange(GenerateCards(CardType.Spade_Peak));
            }
        }
    }

    public void DealFromStock()
    {
        if (StockDealt == 5)
            return;

        for (int i = 0; i < 10; i++)
        {
            Columns[i].AddCard(AllCards[DealtCardsIndex]);

            AllCards[DealtCardsIndex].Fliped = true;
            AllCards[DealtCardsIndex].Pickable = true;

            Columns[i].RefreshPickable();
            DealtCardsIndex++;
        }
        StockDealt++;

        //DealButtonText.text = "Deal Another - " + (5 - StockDealt);
    }

    void DealCards()
    {
        for (int i = 0; i < 10; i++)
        {
            int unFlipedCards = 5;
            if (i > 3)
            {
                unFlipedCards = 4;
            }

            Columns[i].AddCards(AllCards.GetRange(DealtCardsIndex, unFlipedCards));
            DealtCardsIndex += unFlipedCards;

            AllCards[DealtCardsIndex].Fliped = true;
            AllCards[DealtCardsIndex].Pickable = true;
            Columns[i].AddCard(AllCards[DealtCardsIndex]);
            DealtCardsIndex++;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    CardMove[] GenerateCards(CardType cardType)
    {
        CardMove[] cards = new CardMove[13];
        Sprite[] sprites = null;

        switch (cardType)
        {
            case CardType.Clubs_Krest:
                sprites = clubs.cards.Select(x => x.Sprite).ToArray();
                break;
            case CardType.Spade_Peak:
                sprites = spades.cards.Select(x => x.Sprite).ToArray();
                break;
            case CardType.Diamonds_Bubna:
                sprites = diamonds.cards.Select(x => x.Sprite).ToArray();
                break;
            case CardType.Heart_Cherv:
                sprites = hearts.cards.Select(x => x.Sprite).ToArray();
                break;
        }

        for (int i = 0; i < 13; i++)
        {
            CardMove newCard = Instantiate(CardPrefab,
                CardParent.transform);

            cards[i] = newCard;
            cards[i].Value = i;
            cards[i].CardType = cardType;
            cards[i].CarbackSprite = Cardback;
            cards[i].Sprite = sprites[i];

            cards[i].Fliped = false;
        }

        return (cards);
    }

    /// <summary>
    /// Knuth Shuffle
    /// </summary>
    /// <param name="cards"></param>
    void Shuffle(List<CardMove> cards)
    {
        System.Random random = new System.Random();
        int n = cards.Count - 1;
        while (n > 1)
        {
            int k = random.Next(n);
            CardMove temp = cards[n];
            cards[n] = cards[k];
            cards[k] = temp;
            n--;
        }
    }

    public void SortDropedCard(CardMove card)
    {
        var colX = card.transform.position.x / 1.1f;

        int colNum = Mathf.RoundToInt(colX);

        if (Columns[colNum].CanBeDroped(card))
        {
            if (card.Children != null)
                card.ParentColumn.RemoveCards(card.Children);
            card.ParentColumn.RemoveCard(card);
            card.ParentColumn.RefreshPickable();

            Columns[colNum].AddCard(card);
            if (card.Children != null)
                Columns[colNum].AddCards(card.Children);

            Columns[colNum].RefreshPickable();
            Columns[colNum].CheckFinishedSequence();

        }
        else
        {
            card.ReturnToOriginalPosition();
        }
    }
}

public enum DealSettings
{
    OneSuit,
    TwoSuit,
    FourSuit
}
