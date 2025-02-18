using DG.Tweening.Core.Easing;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class CardMove : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public CardType CardType;
    public int Value;
    public Sprite Sprite;
    public Sprite CarbackSprite;
    public Transform TransformParent;

    //Questionable
    public CardColumn ParentColumn;

    public List<CardMove> Children = new List<CardMove>();

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
                SpriteRenderer.color = Color.gray;
            else
                SpriteRenderer.color = Color.white;
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
                SpriteRenderer.sprite = Sprite;
            else
                SpriteRenderer.sprite = CarbackSprite;
        }
    }

    private bool fliped;
    private bool pickable = true;
    private bool picked;

    private Image spriteRenderer;
    private RectTransform rectTransform => transform.GetComponent<RectTransform>();
    private CanvasGroup canvasGroup => GetComponent<CanvasGroup>();

    private Image SpriteRenderer
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

    private void OnMouseDown()
    {
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //dragOffset = this.transform.position - mousePos;

        ////ParentColumn.VerticalLayoutGroup.enabled = false;

        //picked = false;
        //originalPosition = transform.position;

        //Debug.Log(Value + "//");

        //Children = ParentColumn.GetChildrenCards(this);
    }
    private void OnMouseDrag()
    {
        //if (!Pickable)
        //    return;

        //picked = true;

        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Vector3 newPosition = mousePos + dragOffset;

        //this.transform.position = new Vector2(newPosition.x, newPosition.y);

        //Debug.Log(Value + "//");

        //MoveChildren(newPosition);

        ////When card is Draged
        //SetZOrder(110);

        //if (Children != null)
        //    for (int i = 0; i < Children.Count; i++)
        //    {
        //        Children[i].SetZOrder(111 + i);
        //    }
    }
    private void OnMouseUp()
    {
        //if (!picked)
        //    return;
        //CardManager.Instance?.SortDropedCard(this);

        //ParentColumn.VerticalLayoutGroup.enabled = true;
    }

    //public void SetZOrder(int orderInLayer)
    //{
    //    this.transform.position = new Vector3(transform.position.x,
    //        transform.position.y,
    //        orderInLayer * -1);
    //}

    public void ReturnToOriginalPosition()
    {
        ParentColumn.VerticalLayoutGroup.enabled = true;
        transform.SetParent(ParentColumn.ContentScrollView);

        if (Children != null)
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].canvasGroup.blocksRaycasts = true;
                Children[i].transform.SetParent(ParentColumn.ContentScrollView);
            }

        //MoveChildren(originalPosition);

        ParentColumn.RefreshRenderOrder();

        ParentColumn.VerticalLayoutGroup.enabled = true;
    }

    private void MoveChildren(Vector2 newPos)
    {
        if (Children != null)
            //for (int i = 0; i < Children.Count; i++)
            //{
            //    Children[i].rectTransform.anchoredPosition = new Vector2(newPos.x, newPos.y + (i + 1) * ParentColumn.YCardOffset);
            //}
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].rectTransform.anchoredPosition = new Vector2(newPos.x, newPos.y + (i + 1) * ParentColumn.YCardOffset);
            }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;

        picked = false;

        Children = ParentColumn.GetChildrenCards(this);

        transform.SetParent(TransformParent);

        if (Children != null)
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].canvasGroup.blocksRaycasts = false;
                Children[i].transform.SetParent(transform);
            }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!picked)
            return;
        CardManager.Instance?.SortDropedCard(eventData, this);

        canvasGroup.blocksRaycasts = true;

        transform.SetParent(ParentColumn.ContentScrollView);

        if(Children != null)
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].canvasGroup.blocksRaycasts = true;
                Children[i].transform.SetParent(Children[i].ParentColumn.ContentScrollView);
            }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!Pickable)
            return;

        picked = true;

        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector3 newPosition = mousePos + dragOffset;
        //this.transform.position = new Vector2(newPosition.x, newPosition.y);

        rectTransform.anchoredPosition += eventData.delta;

        //When card is Draged
        //SetZOrder(110);

        //if (Children != null)
        //    for (int i = 0; i < Children.Count; i++)
        //    {
        //        Children[i].SetZOrder(111 + i);
        //    }
    }
}
