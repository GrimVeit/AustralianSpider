using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSessionView : View
{
    [SerializeField] private Transform transformDisplay;
    [SerializeField] private Vector3 vectorRotateInFirstUser;
    [SerializeField] private Vector3 vectorRotateInSecondUser;
    [SerializeField] private float durationRotate;

    public void ActivateFirstUserDisplay()
    {
        transformDisplay.DOLocalRotate(vectorRotateInFirstUser, durationRotate);
    }

    public void ActivateSecondUserDisplay()
    {
        transformDisplay.DOLocalRotate(vectorRotateInSecondUser, durationRotate);
    }
}
