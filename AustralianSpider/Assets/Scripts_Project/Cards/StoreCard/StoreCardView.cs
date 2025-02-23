using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StoreCardView : View
{
    public int sendCardCount;
    public event Action<List<CardInteractive>> OnDealCards_Value;
    public event Action<List<CardInteractive>> OnDealCardsFromStock_Value;
    public event Action OnDealCardsFromStock;

    [SerializeField] private Button buttonTest;
    [SerializeField] private CardInteractive cardMovePrefab;
    [SerializeField] private Transform transformCardParent;
    [SerializeField] private List<Transform> transformCardsGroup;
    [SerializeField] private Transform transformDestroy;

    private GameType currentGameType;
    private CoverCardDesign currentCoverCardDesign;
    private FaceCardDesign currentFaceCardDesign;

    private List<CardInteractive> allCardInteractives = new List<CardInteractive>();

    private List<List<CardInteractive>> cardGroupCards = new List<List<CardInteractive>>();

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

        int[] sizes = { 10, 10, 10, 10, 10, 54 };
        int index = 0;

        for (int i = 0; i < sizes.Length; i++)
        {
            List<CardInteractive> sublist = new List<CardInteractive>();

            for (int j = 0; j < sizes[i]; j++)
            {
                sublist.Add(allCardInteractives[index]);
                index += 1;
            }

            MoveCardToTransform(sublist, transformCardsGroup[i]);

            cardGroupCards.Add(sublist);
        }
    }

    public void DealCards()
    {
        OnDealCards_Value?.Invoke(cardGroupCards[^1]);
        cardGroupCards.RemoveAt(cardGroupCards.Count - 1);
    }

    public void DealCardsFromStock()
    {
        if(cardGroupCards.Count == 0) return;

        OnDealCardsFromStock_Value?.Invoke(cardGroupCards[^1]);
        OnDealCardsFromStock?.Invoke();

        cardGroupCards.RemoveAt(cardGroupCards.Count - 1);
    }

    public void DestroyCards(List<CardInteractive> cards)
    {
        Coroutines.Start(DestroyCards_Coro(cards));
    }

    private IEnumerator DestroyCards_Coro(List<CardInteractive> cards)
    {
        for (int i = cards.Count - 1; i >= 0; i--)
        {
            cards[i].transform.SetParent(transformDestroy);
            cards[i].MoveTo(transformDestroy.position, 0.2f, null);
            cards[i].RotateTo(new Vector3(0, 0, -90), 0.3f, null);

            yield return new WaitForSeconds(0.03f);
        }
    }

    private void MoveCardToTransform(List<CardInteractive> cardInteractives, Transform transformPos)
    {
        for (int i = 0; i < cardInteractives.Count; i++)
        {
            Debug.Log(transformPos.name);

            cardInteractives[i].transform.SetParent(transformPos);
            cardInteractives[i].transform.localPosition = Vector3.zero;
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
