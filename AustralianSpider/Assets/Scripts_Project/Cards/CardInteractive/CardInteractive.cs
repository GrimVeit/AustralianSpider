using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
                image.sprite = Sprite;
            else
                image.sprite = CarbackSprite;
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

    private bool fliped;
    private bool pickable = false;
    private bool picked;

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

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!Pickable)
            return;

        if (!picked)
            return;

        canvasGroup.blocksRaycasts = true;

        transform.SetParent(ParentColumn.ContentScrollView);

        if (Children != null)
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].canvasGroup.blocksRaycasts = true;
                Children[i].transform.SetParent(Children[i].ParentColumn.ContentScrollView);
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



    #region Input

    public event Action<CardInteractive> OnPickedCard;
    public event Action<PointerEventData, CardInteractive> OnDroppedCard;

    #endregion
}
