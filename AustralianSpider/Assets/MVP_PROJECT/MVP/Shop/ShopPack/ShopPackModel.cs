using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPackModel
{
    public event Action OnBuyRandomSpin;

    public event Action OnShow;
    public event Action OnHide;
    public event Action<int> OnGetData;

    private IMoneyProvider moneyProvider;

    private int price;

    public ShopPackModel(IMoneyProvider moneyProvider, int price)
    {
        this.moneyProvider = moneyProvider;
        this.price = price;
    }

    public void BuyPack()
    {
        if (moneyProvider.CanAfford(price))
        {
            OnBuyRandomSpin?.Invoke();

            moneyProvider.SendMoney(-price);
        }
    }

    public void ShowBuy()
    {
        OnShow?.Invoke();
    }

    public void HideBuy()
    {
        OnHide?.Invoke();
    }
}
