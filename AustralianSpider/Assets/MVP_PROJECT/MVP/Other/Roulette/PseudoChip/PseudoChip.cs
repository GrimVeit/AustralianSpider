using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PseudoChip : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ChipData ChipData => chipData;

    public event Action<PseudoChip> OnGrabbing;
    public event Action OnStartMove;
    public event Action<Transform> OnEndMove;
    public event Action<Vector2> OnMove;

    [SerializeField] private ChipData chipData;

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

    #region Methods

    public void Teleport()
    {
        canvasGroup.blocksRaycasts = true;

        rectTransform.localPosition = Vector2.zero;
    }

    public void StartMove()
    {
        if (moveTween != null)
            moveTween.Kill();

        canvasGroup.blocksRaycasts = false;
    }

    public void EndMove()
    {
        canvasGroup.blocksRaycasts = true;

        rectTransform.DOLocalMove(Vector2.zero, 0.1f);
    }


    public void Move(Vector2 vector)
    {
        rectTransform.anchoredPosition += vector;
    }

    #endregion

    #region Input

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnGrabbing?.Invoke(this);
        OnStartMove?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnMove?.Invoke(eventData.delta);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnEndMove?.Invoke(transform);
    }

    #endregion
}
