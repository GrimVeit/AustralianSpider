using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoreCardView : View
{
    public event Action<List<CardInteractive>> OnDealCards;


    [SerializeField] private CardInteractive cardMovePrefab;
    [SerializeField] private Transform transformCardParent;

    private GameType currentGameType;
    private CoverCardDesign currentCoverCardDesign;
    private FaceCardDesign currentFaceCardDesign;

    [SerializeField] private List<CardInteractive> allCardInteractives = new List<CardInteractive>();

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
        OnDealCards?.Invoke(allCardInteractives.Take(54).ToList());
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
