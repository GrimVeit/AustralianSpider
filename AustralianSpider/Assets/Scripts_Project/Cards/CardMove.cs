using DG.Tweening.Core.Easing;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardMove : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public CardType CardType;
    public int Value;
    public Sprite Sprite;
    public Sprite CarbackSprite;

    //Questionable
    public CardColumn ParentColumn;

    private Vector3 dragOffset;
    private Vector3 originalPosition;

    public List<CardMove> Children;

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

    public void SetZOrder(int orderInLayer)
    {
        this.transform.position = new Vector3(transform.position.x,
            transform.position.y,
            orderInLayer * -1);
    }

    private void OnMouseUp()
    {
        //if (!picked)
        //    return;
        //CardManager.Instance?.SortDropedCard(this);

        //ParentColumn.VerticalLayoutGroup.enabled = true;
    }

    public void ReturnToOriginalPosition()
    {
        this.transform.position = originalPosition;

        MoveChildren(originalPosition);

        //ParentColumn.RefreshRenderOrder();

        //ParentColumn.VerticalLayoutGroup.enabled = true;
    }

    private void MoveChildren(Vector2 newPos)
    {
        if (Children != null)
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].transform.position = new Vector2(newPos.x, newPos.y + (i + 1) * ParentColumn.YCardOffset);
            }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragOffset = this.transform.position - mousePos;

        //ParentColumn.VerticalLayoutGroup.enabled = false;

        picked = false;
        originalPosition = transform.position;

        Debug.Log(Value + "//");

        Children = ParentColumn.GetChildrenCards(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!picked)
            return;
        CardManager.Instance?.SortDropedCard(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!Pickable)
            return;

        picked = true;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 newPosition = mousePos + dragOffset;

        this.transform.position = new Vector2(newPosition.x, newPosition.y);

        Debug.Log(Value + "//");

        MoveChildren(newPosition);

        //When card is Draged
        SetZOrder(110);

        if (Children != null)
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].SetZOrder(111 + i);
            }
    }
}
