using DG.Tweening;
using System;
using UnityEngine;

public class CardSpawnerView : View, IIdentify
{
    public string GetID() => ID;
    public event Action OnSpawnCard;

    [SerializeField] private string ID;
    [SerializeField] private CardView cardViewPrefab;
    [SerializeField] private CardDropZone cardDropZone;
    [SerializeField] private Transform dropCardObject;

    [SerializeField] private Transform cardViewParent;
    private CardView currentCardView;


    public void Initialize()
    {
        cardDropZone.OnSpawnCard += HandlerSpawnCard;
    }

    public void Dispose()
    {
        cardDropZone.OnSpawnCard -= HandlerSpawnCard;
    }

    public void SpawnCard(CardValue cardValue)
    {
        currentCardView = Instantiate(cardViewPrefab, cardViewParent);
        currentCardView.transform.SetLocalPositionAndRotation(Vector3.zero, cardViewPrefab.transform.rotation);
        currentCardView.SetData(cardValue);
    }

    public void DestroyCard()
    {
        Destroy(currentCardView.gameObject);
    }

    private void HandlerSpawnCard()
    {
        OnSpawnCard?.Invoke();
    }
}
