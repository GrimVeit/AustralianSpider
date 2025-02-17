using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasGroup))]

public class CardDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler
{
    public event Action OnStartGrab;
    public event Action OnStartMove;
    public event Action<PointerEventData> OnEndMove;
    public event Action<Vector2> OnMove;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private Tween moveTween;

    public void Initialize()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Dispose()
    {

    }

    public void Teleport(Vector3 vector)
    {
        rectTransform.localPosition = vector;
    }

    public void StartMove()
    {
        if (moveTween != null)
            moveTween.Kill();

        canvasGroup.blocksRaycasts = false;
    }

    public void EndMove(Vector3 vector)
    {
        canvasGroup.blocksRaycasts = true;

        rectTransform.DOLocalMove(vector, 0.1f);
    }


    public void Move(Vector2 vector)
    {
        rectTransform.anchoredPosition += vector;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        OnStartMove?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnMove?.Invoke(eventData.delta);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnEndMove?.Invoke(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnStartGrab?.Invoke();
    }
}
