using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopItemPack : MonoBehaviour
{
    public Transform Transform => transformItem;
    [SerializeField] private Transform transformItem;

    public event Action OnEndSelect;
    public event Action OnSelectPack;
    public Pack Pack;

    private float timeMove = 0.5f;

    private Tween tweenMove;
    private Tween tweenScale;
    private Tween tweenRotate;

    private Transform transformParentDefault;

    private void Awake()
    {
        transformParentDefault = transform.parent;
    }

    public void SelectPack(Vector2 vectorMove, Vector2 vectorScale)
    {
        tweenMove?.Kill();
        tweenScale?.Kill();
        tweenRotate?.Kill();

        tweenMove = transform.DOMove(vectorMove, timeMove).OnComplete(()=> OnEndSelect?.Invoke());
        tweenScale = transform.DOScale(vectorScale, timeMove);
        tweenRotate = transform.DOLocalRotate(Vector3.zero, timeMove);
    }

    public void UnselectPack()
    {
        tweenMove?.Kill();
        tweenScale?.Kill();
        tweenRotate?.Kill();

        tweenMove = transform.DOLocalMove(Vector3.zero, timeMove);
        tweenScale = transform.DOScale(Vector2.one, timeMove);
        tweenRotate = transform.DOLocalRotate(Vector3.zero, timeMove);
    }

    public void SetParent(Transform transformParent)
    {
        transform.SetParent(transformParent);
    }

    public void SetDefaultParent()
    {
        transform.SetParent(transformParentDefault);
    }
}

[Serializable]
public class Pack
{
    public List<TypeCard> Items;
    public Sprite SpritePack;
    public int Coins;
}

public enum TypeCard
{
    common, rare, epic, legendary, gold
}
