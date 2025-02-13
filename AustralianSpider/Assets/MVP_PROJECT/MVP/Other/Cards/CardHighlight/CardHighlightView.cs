using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHighlightView : View, IIdentify
{
    public string GetID() => ID;

    [SerializeField] private string ID;
    [SerializeField] private Transform dropCardObject;

    private Vector3 normalScale;
    private Tween scaleTween;

    public void Initialize()
    {
        normalScale = dropCardObject.localScale;
    }

    public void Dispose()
    {

    }

    public void ActivateChooseHighlight()
    {
        scaleTween = dropCardObject.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.6f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
    }

    public void DeactivateChooseHighlight()
    {
        if (scaleTween != null)
            scaleTween.Kill();
        dropCardObject.localScale = normalScale;
    }
}
