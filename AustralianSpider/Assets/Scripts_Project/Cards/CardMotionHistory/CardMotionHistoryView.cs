using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardMotionHistoryView : View
{
    [SerializeField] private Button buttonReturmLastMotion;
    [SerializeField] private Image imageButton;
    [SerializeField] private Sprite spriteActive;
    [SerializeField] private Sprite spriteDeactive;

    public void Initialize()
    {
        buttonReturmLastMotion.onClick.AddListener(()=> OnClickToReturnButton?.Invoke());
    }

    public void Dispose()
    {
        buttonReturmLastMotion.onClick.RemoveListener(()=> OnClickToReturnButton?.Invoke());
    }

    public void Activate()
    {
        imageButton.sprite = spriteActive;
    }

    public void Deactivate()
    {
        imageButton.sprite = spriteDeactive;
    }

    #region Input

    public event Action OnClickToReturnButton;

    #endregion
}
