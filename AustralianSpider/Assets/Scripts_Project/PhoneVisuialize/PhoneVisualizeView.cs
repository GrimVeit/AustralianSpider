using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PhoneVisualizeView : View
{
    [SerializeField] private Transform transformPhone;
    [SerializeField] private Image imagePhone;

    [SerializeField] private Vector3 scaleMinVector;
    [SerializeField] private Vector3 scaleMaxLandscape;
    [SerializeField] private Vector3 scaleMaxPortrait;

    [SerializeField] private Vector3 rotateLandscapeVector;
    [SerializeField] private Vector3 rotatePortraitVector;

    private Sequence sequenceVisualize;

    public void LandscapeToPortrait()
    {
        sequenceVisualize?.Kill();

        sequenceVisualize = DOTween.Sequence();

        transformPhone.localScale = scaleMaxLandscape;
        transformPhone.localEulerAngles = rotateLandscapeVector;

        sequenceVisualize
            .Append(transformPhone.DOScale(scaleMinVector, 0.2f))
            .Join(imagePhone.DOFade(1, 0.2f))
            .Append(transformPhone.DORotate(rotatePortraitVector, 0.2f))
            .Append(transformPhone.DOScale(scaleMaxPortrait, 0.2f))
            .Join(imagePhone.DOFade(0, 0.2f)).OnComplete(()=> OnCompleteMoveFromLandscapeToPortrait?.Invoke());
    }

    public void PortraitToLandscape()
    {
        sequenceVisualize?.Kill();

        sequenceVisualize = DOTween.Sequence();

        transformPhone.localScale = scaleMaxPortrait;
        transformPhone.localEulerAngles = rotatePortraitVector;

        sequenceVisualize
            .Append(transformPhone.DOScale(scaleMinVector, 0.2f))
            .Join(imagePhone.DOFade(1, 0.2f))
            .Append(transformPhone.DORotate(rotateLandscapeVector, 0.2f))
            .Append(transformPhone.DOScale(scaleMaxLandscape, 0.2f))
            .Join(imagePhone.DOFade(0, 0.2f)).OnComplete(() => OnCompleteMoveFromPortraitToLandscape?.Invoke());
    }

    #region Input

    public event Action OnCompleteMoveFromPortraitToLandscape;
    public event Action OnCompleteMoveFromLandscapeToPortrait;

    #endregion
}
