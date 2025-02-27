using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PhoneVisualize : MonoBehaviour
{
    public void LocalMoveTo(Vector3 vector, float time, Action onComplete = null)
    {
        transform.DOLocalMove(vector, time).OnComplete(()=> onComplete?.Invoke());
    }

    public void LocalScaleTo(Vector3 vector, float time, Action onComplete = null)
    {
        transform.DOScale(vector, time).OnComplete(()=> onComplete?.Invoke());
    }

    public void LocalRotateTo(Vector3 vector, float time, Action onComplete = null)
    {
        transform.DOLocalRotate(vector, time).OnComplete(()=> onComplete?.Invoke());
    }
}
