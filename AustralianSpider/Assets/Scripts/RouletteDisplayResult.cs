using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class RouletteDisplayResult : MonoBehaviour
{
    public event Action OnStartShowResult;
    public event Action OnFinishShowResult;
    public event Action OnStartHideResult;
    public event Action OnFinishHideResult;

    [SerializeField] private Image imageResult;

    private Vector3 normalScale;

    public void Initialize()
    {
        normalScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    public void Dispose()
    {

    }

    public void SetData(Sprite sprite)
    {
        imageResult.enabled = true;
        imageResult.sprite = sprite;
    }

    public void Show(Vector3 from, Vector3 to)
    {
        OnStartShowResult?.Invoke();
        transform.localScale = Vector3.zero;
        transform.localPosition = from;
        transform.DOScale(normalScale, 0.3f);
        transform.DOMove(to, 0.5f).OnComplete(() => OnFinishShowResult?.Invoke()); ; //() => OnFinishShowResult?.Invoke()
    }

    public void Hide(Vector3 to)
    {
        OnStartHideResult?.Invoke();
        imageResult.enabled = false;
        transform.DOScale(Vector3.zero, 0.2f);
        transform.DOMove(to, 0.3f).OnComplete(()=> OnFinishHideResult?.Invoke()); //OnFinishHideResult?.Invoke()
    }
}
