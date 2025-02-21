using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardMotionHistoryView : View
{
    [SerializeField] private Button buttonReturmLastMotion;

    public void Initialize()
    {
        buttonReturmLastMotion.onClick.AddListener(()=> OnClickToReturnButton?.Invoke());
    }

    public void Dispose()
    {
        buttonReturmLastMotion.onClick.RemoveListener(()=> OnClickToReturnButton?.Invoke());
    }

    #region Input

    public event Action OnClickToReturnButton;

    #endregion
}
