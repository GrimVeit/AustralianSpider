using DG.Tweening;
using System;
using UnityEngine;

public class CardMoveAI : MonoBehaviour
{
    public event Action OnEndMove;

    private RectTransform rectTransform;

    private Tween moveTween;

    public void Initialize()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Dispose()
    {

    }

    public void Teleport(Vector3 vector)
    {
        if (moveTween != null)
            moveTween.Kill();

        rectTransform.localPosition = vector;
    }

    public void StartMove(Vector3 vector, float speed)
    {
        moveTween = rectTransform.DOMove(vector, speed).OnComplete(() =>
        {
            OnEndMove?.Invoke();
        });
    }
}
