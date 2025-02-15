using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyGameDesignView : View
{
    [SerializeField] private Button buttonBuyGameDesign;

    public void Initialize()
    {
        buttonBuyGameDesign.onClick.AddListener(() => OnBuyGameDesign?.Invoke());
    }

    public void Dispose()
    {
        buttonBuyGameDesign.onClick.RemoveListener(() => OnBuyGameDesign?.Invoke());
    }

    #region Input

    public event Action OnBuyGameDesign;

    #endregion
}
