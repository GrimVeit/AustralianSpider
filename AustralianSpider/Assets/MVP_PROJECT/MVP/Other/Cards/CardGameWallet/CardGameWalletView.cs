using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardGameWalletView : View
{
    [SerializeField] private Button buttonTransferMoneyToBank;
    [SerializeField] private List<BankDisplayView> displayesAllMoney = new List<BankDisplayView>();
    [SerializeField] private BankDisplayView displayAddMoney;
    [SerializeField] private BankDisplayView displayRemoveMoney;

    public void Initialize()
    {
        for (int i = 0; i < displayesAllMoney.Count; i++)
        {
            displayesAllMoney[i].Initialize();
        }

        buttonTransferMoneyToBank.onClick.AddListener(HandlerClickToTransferMoneyToBank);
    }

    public void Dispose()
    {
        for (int i = 0; i < displayesAllMoney.Count; i++)
        {
            displayesAllMoney[i].Dispose();
        }

        buttonTransferMoneyToBank.onClick.RemoveListener(HandlerClickToTransferMoneyToBank);
    }

    public void SendMoneyDisplay(int coins)
    {
        for (int i = 0; i < displayesAllMoney.Count; i++)
        {
            displayesAllMoney[i].SendMoneyDisplay(coins);
        }
    }

    public void OnAddMoneyDisplay(int coins)
    {
        displayAddMoney.SendMoneyDisplay(coins);
    }

   public void OnRemoveMoneyDisplay(int coins) 
    {
        displayRemoveMoney.SendMoneyDisplay(-coins);
    }

    #region Input

    public event Action OnClickToTransferMoneyToBankButton;

    private void HandlerClickToTransferMoneyToBank()
    {
        OnClickToTransferMoneyToBankButton?.Invoke();
    }

    #endregion
}
