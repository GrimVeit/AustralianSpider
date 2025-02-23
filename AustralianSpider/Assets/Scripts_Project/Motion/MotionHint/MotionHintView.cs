using System;
using UnityEngine;
using UnityEngine.UI;

public class MotionHintView : View
{
    [SerializeField] private Button buttonHint;

    public void Initialize()
    {
        buttonHint.onClick.AddListener(()=> OnClickToHint?.Invoke());
    }

    public void Dispose()
    {
        buttonHint.onClick.RemoveListener(()=> OnClickToHint?.Invoke());
    }

    #region Input

    public event Action OnClickToHint;

    #endregion
}
