using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyFaceCardDesignView : View
{
    [SerializeField] private Button buttonBuyFaceCardDesign;

    public void Initialize()
    {
        buttonBuyFaceCardDesign.onClick.AddListener(() => OnBuyFaceCardDesign?.Invoke());
    }

    public void Dispose()
    {
        buttonBuyFaceCardDesign.onClick.RemoveListener(() => OnBuyFaceCardDesign?.Invoke());
    }

    #region Input

    public event Action OnBuyFaceCardDesign;

    #endregion
}
