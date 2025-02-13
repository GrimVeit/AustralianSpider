using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPackView : View
{
    [SerializeField] private GameObject objectBuy;
    [SerializeField] private Button buttonBuy;
    [SerializeField] private float timeScale;
    [SerializeField] private TextMeshProUGUI textCoins;

    private Tween tweenScale;

    public void Initialize()
    {
        buttonBuy.onClick.AddListener(HandleClickToBuyButton);
    }

    public void Dispose()
    {
        buttonBuy.onClick.RemoveListener(HandleClickToBuyButton);
    }

    public void Show()
    {
        objectBuy.SetActive(true);
        buttonBuy.enabled = true;
        tweenScale = objectBuy.transform.DOScale(Vector3.one, timeScale);
    }

    public void Hide()
    {
        if(tweenScale != null)
        {
            tweenScale.Kill();
            objectBuy.SetActive(false);
        }

        buttonBuy.enabled = false;
        tweenScale = objectBuy.transform.DOScale(Vector3.zero, timeScale).OnComplete(()=> objectBuy.SetActive(false));
    }

    public void SetCoins(int coins)
    {
        textCoins.text = coins.ToString();
    }

    #region Input

    public event Action OnClickToBuyButton;

    private void HandleClickToBuyButton()
    {
        OnClickToBuyButton?.Invoke();
    }

    #endregion
}
