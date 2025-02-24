using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class CardInteractive : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public bool Pickable
    {
        get
        {
            return pickable;
        }
        set
        {
            pickable = value;

            if (!pickable)
            {
                canvasGroup.blocksRaycasts = false;
                image.color = Color.gray;
            }
            else
            {
                canvasGroup.blocksRaycasts = true;
                image.color = Color.white;
            }
        }
    }
    public bool Fliped
    {
        get
        {
            return fliped;
        }
        set
        {
            fliped = value;

            if (value)
                ActivateFlip();
            else
                DeactivateFlip();
        }
    }

    public List<CardInteractive> Children = new List<CardInteractive>();


    public CardType CardType => currentCardData.CardType;
    public int Value => currentCardData.CardId;
    public Sprite Sprite => currentCardData.Sprite;
    public Sprite CarbackSprite;
    public Card currentCardData;

    //Questionable
    public Column ParentColumn;

    [SerializeField] private Image imageBorder;

    private bool fliped;
    private bool pickable = false;
    private bool picked;

    private Sequence sequenceFlip;
    private Sequence sequenceSelect;

    private Image spriteRenderer;
    private RectTransform rectTransform => transform.GetComponent<RectTransform>();
    private CanvasGroup canvasGroup => GetComponent<CanvasGroup>();

    private Image image
    {
        get
        {
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<Image>();

            return spriteRenderer;
        }
        set
        {
            spriteRenderer = value;
        }
    }

    public void SetData(Card card, Sprite spriteCover, Transform transformParent)
    {
        this.currentCardData = card;
        CarbackSprite = spriteCover;

        image.sprite = spriteCover;
    }

    public void MoveTo(Vector3 vector, float speed, Action actionToEnd)
    {
        transform.DOMove(vector, speed).OnComplete(()=> actionToEnd?.Invoke());
    }

    public void RotateTo(Vector3 vector, float speed, Action actionToEnd)
    {
        transform.DORotate(vector, speed).OnComplete(() => actionToEnd?.Invoke());
    }

    public void MoveBack(float speed, Action actionToEnd)
    {
        transform.DOLocalMove(ParentColumn.NewCardPosition, speed).OnComplete(()=> actionToEnd?.Invoke());
    }

    public void SetParentColumn(Column cardColumn)
    {
        ParentColumn = cardColumn;
    }

    public void ReturnToOriginalPosition()
    {
        ParentColumn.VerticalLayoutGroup.enabled = true;
        transform.SetParent(ParentColumn.ContentScrollView);

        canvasGroup.blocksRaycasts = true;

        if (Children != null)
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].canvasGroup.blocksRaycasts = true;
                Children[i].transform.SetParent(ParentColumn.ContentScrollView);
            }

        ParentColumn.VerticalLayoutGroup.enabled = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!Pickable)
            return;

        canvasGroup.blocksRaycasts = false;

        picked = true;

        Children = ParentColumn.GetChildrenCards(this);
        if (Children != null)
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].canvasGroup.blocksRaycasts = false;
                Children[i].transform.SetParent(transform);
            }

        OnPickedCard?.Invoke(this);
    }

    public void SetChildrens(List<CardInteractive> children)
    {
        if (children != null)
        for (int i = 0; i < children.Count; i++)
        {
            children[i].canvasGroup.blocksRaycasts = false;
            children[i].transform.SetParent(transform);
        }
    }

    public void CleanChildrens()
    {
        if(Children != null)
        for (int i = 0; i < Children.Count; i++)
        {
            Children[i].canvasGroup.blocksRaycasts = true;
            Children[i].transform.SetParent(Children[i].ParentColumn.ContentScrollView);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!Pickable)
            return;

        if (!picked)
            return;

        canvasGroup.blocksRaycasts = true;

        if (Children != null)
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].canvasGroup.blocksRaycasts = true;
            }

        OnDroppedCard?.Invoke(eventData, this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!Pickable)
            return;

        picked = true;

        rectTransform.anchoredPosition += eventData.delta;
    }


    #region Design

    public void ActivateFlip()
    {
        sequenceFlip?.Kill();

        Debug.Log("ON");

        sequenceFlip = DOTween.Sequence();
        sequenceFlip.Append(transform.DORotate(new Vector3(0, 90, 0), 0.1f).OnComplete(() => image.sprite = Sprite))
            .Append(transform.DORotate(Vector3.zero, 0.1f));  
    }

    public void DeactivateFlip()
    {
        sequenceFlip?.Kill();

        Debug.Log("OFF");

        sequenceFlip = DOTween.Sequence();
        sequenceFlip.Append(transform.DORotate(new Vector3(0, 90, 0), 0.1f).OnComplete(() => image.sprite = CarbackSprite))
            .Append(transform.DORotate(Vector3.zero, 0.1f));
    }

    public void SelectCard()
    {
        sequenceSelect?.Kill();

        sequenceSelect = DOTween.Sequence();

        sequenceSelect.Append(transform.DOScale(1.1f, 0.3f));
        sequenceSelect.Join(imageBorder.DOFade(1f, 0.3f));

        sequenceSelect.Append(transform.DOScale(1f, 0.3f));
        sequenceSelect.Join(imageBorder.DOFade(0f, 0.3f));
    }

    #endregion



    #region Input

    public event Action<CardInteractive> OnPickedCard;
    public event Action<PointerEventData, CardInteractive> OnDroppedCard;

    #endregion
}
