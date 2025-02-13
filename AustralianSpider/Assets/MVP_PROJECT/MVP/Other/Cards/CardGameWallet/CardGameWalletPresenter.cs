using System;
using UnityEngine;

public class CardGameWalletPresenter
{
    private CardGameWalletModel cardGameWalletModel;
    private CardGameWalletView cardGameWalletView;

    public CardGameWalletPresenter(CardGameWalletModel cardGameWalletModel, CardGameWalletView cardGameWalletView)
    {
        this.cardGameWalletModel = cardGameWalletModel;
        this.cardGameWalletView = cardGameWalletView;
    }

    public void Initialize()
    {
        ActivateEvents();

        cardGameWalletModel.Initialize();
        cardGameWalletView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        cardGameWalletModel.Dispose();
        cardGameWalletView.Dispose();
    }

    private void ActivateEvents()
    {
        cardGameWalletView.OnClickToTransferMoneyToBankButton += cardGameWalletModel.TransitMoneyToBank;

        cardGameWalletModel.OnChangeMoney += cardGameWalletView.SendMoneyDisplay;
        cardGameWalletModel.OnAddMoney += cardGameWalletView.OnAddMoneyDisplay;
        cardGameWalletModel.OnRemoveMoney += cardGameWalletView.OnRemoveMoneyDisplay;
    }

    private void DeactivateEvents()
    {
        cardGameWalletView.OnClickToTransferMoneyToBankButton -= cardGameWalletModel.TransitMoneyToBank;

        cardGameWalletModel.OnChangeMoney -= cardGameWalletView.SendMoneyDisplay;
        cardGameWalletModel.OnAddMoney -= cardGameWalletView.OnAddMoneyDisplay;
        cardGameWalletModel.OnRemoveMoney -= cardGameWalletView.OnRemoveMoneyDisplay;
    }

    #region Input

    public void SetBet(int bet)
    {
        cardGameWalletModel.SetBet(bet);
    }

    public void IncreaseMoney()
    {
        cardGameWalletModel.IncreseMoney();
    }

    public void DecreaseMoney()
    {
        cardGameWalletModel.DecreaseMoney();
    }

    public void TransitMoneyToBank()
    {
        cardGameWalletModel.TransitMoneyToBank();
    }

    public event Action OnMoneySuccesTransitToBank
    {
        add { cardGameWalletModel.OnMoneySuccesTransitToBank += value; }
        remove { cardGameWalletModel.OnMoneySuccesTransitToBank -= value; }
    }

    #endregion
}
