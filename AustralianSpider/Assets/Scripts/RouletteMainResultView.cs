using DG.Tweening;
using System;
using UnityEngine;

public class RouletteMainResultView : MonoBehaviour
{
    public event Action OnStartShowResult;
    public event Action OnFinishShowResult;
    public event Action OnStartHideResult;
    public event Action OnFinishHideResult;

    [SerializeField] private Transform from;
    [SerializeField] private Transform to;

    private Vector3 normalScale;

    public void Initialize()
    {
        normalScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    public void Dispose()
    {

    }

    public void Show()
    {
        OnStartShowResult?.Invoke();
        transform.localScale = Vector3.zero;
        transform.localPosition = from.position;
        transform.DOScale(normalScale, 0.3f);
        transform.DOMove(to.position, 0.5f).OnComplete(() => OnFinishShowResult?.Invoke());
    }

    public void Hide()
    {
        OnStartHideResult?.Invoke();
        transform.DOScale(Vector3.zero, 0.2f);
        transform.DOMove(to.position, 0.3f).OnComplete(() => OnFinishHideResult?.Invoke());
    }
}
