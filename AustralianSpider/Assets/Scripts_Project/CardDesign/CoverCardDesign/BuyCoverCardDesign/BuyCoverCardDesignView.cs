using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyCoverCardDesignView : View
{
    [SerializeField] private Button buttonBuyCoverCardDesign;

    public void Initialize()
    {
        buttonBuyCoverCardDesign.onClick.AddListener(() => OnBuyCoverCardDesign?.Invoke());
    }

    public void Dispose()
    {
        buttonBuyCoverCardDesign.onClick.RemoveListener(() => OnBuyCoverCardDesign?.Invoke());
    }

    #region Input

    public event Action OnBuyCoverCardDesign;

    #endregion
}
