using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StoreCardView : View
{
    public event Action<List<CardInteractive>> OnDealCards;
    public event Action<List<CardInteractive>> OnDealCardsFromStock;

    [SerializeField] private Button buttonTest;
    [SerializeField] private CardInteractive cardMovePrefab;
    [SerializeField] private Transform transformCardParent;

    private GameType currentGameType;
    private CoverCardDesign currentCoverCardDesign;
    private FaceCardDesign currentFaceCardDesign;

    [SerializeField] private List<CardInteractive> allCardInteractives = new List<CardInteractive>();

    public int sendCardCount;

    public void Initialize()
    {
        buttonTest.onClick.AddListener(DealCardsFromStock);
    }

    public void Dispose()
    {
        buttonTest.onClick.RemoveListener(DealCardsFromStock);
    }

    public void SetGameType(GameType gameType)
    {
        currentGameType = gameType;
    }

    public void SetCoverCardDesign(CoverCardDesign cardDesign)
    {
        currentCoverCardDesign = cardDesign;
    }

    public void SetFaceCardDesign(FaceCardDesign faceCardDesign)
    {
        currentFaceCardDesign = faceCardDesign;
    }

    public void GenerateCards()
    {
        for (int i = 0; i < currentGameType.CardTypes.Count; i++)
        {
            CreateCards(currentGameType.CardTypes[i]);
        }

        ShuffleCards(allCardInteractives);
    }

    public void DealCards()
    {
        OnDealCards?.Invoke(allCardInteractives.GetRange(0, 54));
        sendCardCount = 54;
    }

    public void DealCardsFromStock()
    {
        int startIndex = sendCardCount;

        if(startIndex < allCardInteractives.Count)
        {
            int batchSize = Math.Min(10, allCardInteractives.Count - startIndex);
            OnDealCardsFromStock?.Invoke(allCardInteractives.GetRange(startIndex, batchSize));

            sendCardCount += batchSize;
        }
    }

    private void CreateCards(CardType cardType)
    {
        switch (cardType)
        {
            case CardType.Clubs_Krest:
                for (int i = 0; i < currentFaceCardDesign.Clubs_Krests.cards.Count; i++)
                {
                    var cardInteractive = Instantiate(cardMovePrefab, transformCardParent);
                    cardInteractive.SetData(currentFaceCardDesign.Clubs_Krests.cards[i], currentCoverCardDesign.SpriteDesign, transformCardParent);

                    allCardInteractives.Add(cardInteractive);
                }
                break;

            case CardType.Diamonds_Bubna:
                for (int i = 0; i < currentFaceCardDesign.Diamonds_Bubns.cards.Count; i++)
                {
                    var cardInteractive = Instantiate(cardMovePrefab, transformCardParent);
                    cardInteractive.SetData(currentFaceCardDesign.Diamonds_Bubns.cards[i], currentCoverCardDesign.SpriteDesign, transformCardParent);

                    allCardInteractives.Add(cardInteractive);
                }
                break;

            case CardType.Spade_Peak:
                for (int i = 0; i < currentFaceCardDesign.Spades_Peaks.cards.Count; i++)
                {
                    var cardInteractive = Instantiate(cardMovePrefab, transformCardParent);
                    cardInteractive.SetData(currentFaceCardDesign.Spades_Peaks.cards[i], currentCoverCardDesign.SpriteDesign, transformCardParent);

                    allCardInteractives.Add(cardInteractive);
                }
                break;

            case CardType.Heart_Cherv:
                for (int i = 0; i < currentFaceCardDesign.Hearts_Chervs.cards.Count; i++)
                {
                    var cardInteractive = Instantiate(cardMovePrefab, transformCardParent);
                    cardInteractive.SetData(currentFaceCardDesign.Hearts_Chervs.cards[i], currentCoverCardDesign.SpriteDesign, transformCardParent);

                    allCardInteractives.Add(cardInteractive);
                }
                break;
        }
    }

    private void ShuffleCards(List<CardInteractive> cards)
    {
        System.Random random = new System.Random();
        int n = cards.Count - 1;
        while (n > 1)
        {
            int k = random.Next(n);
            CardInteractive temp = cards[n];
            cards[n] = cards[k];
            cards[k] = temp;
            n--;
        }
    }
}
