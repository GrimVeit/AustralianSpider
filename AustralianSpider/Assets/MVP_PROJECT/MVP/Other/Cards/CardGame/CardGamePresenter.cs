using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class CardGamePresenter
{
    private CardGameModel cardGameModel;
    private CardGameView cardGameView;

    public CardGamePresenter(CardGameModel cardGameModel, CardGameView cardGameView)
    {
        this.cardGameModel = cardGameModel;
        this.cardGameView = cardGameView;
    }

    public void Initialize()
    {
        ActivateEvents();
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        cardGameView.OnClickIncreaseChance += cardGameModel.Increase;
        cardGameView.OnClickDecreaseChance += cardGameModel.Decrease;
        cardGameView.OnClickContinue += cardGameModel.SubmitChance;

        cardGameModel.OnActivate += cardGameView.Activate;
        cardGameModel.OnDeactivate += cardGameView.Deactivate;
        cardGameModel.OnChooseIncrease += cardGameView.ChooseIncrease;
        cardGameModel.OnChooseDecrease += cardGameView.ChooseDecrease;
        cardGameModel.OnChooseChance_Values += cardGameView.OnChoose;
        cardGameModel.OnReset += cardGameView.ResetData;
    }

    private void DeactivateEvents()
    {
        cardGameView.OnClickIncreaseChance -= cardGameModel.Increase;
        cardGameView.OnClickDecreaseChance -= cardGameModel.Decrease;
        cardGameView.OnClickContinue -= cardGameModel.SubmitChance;

        cardGameModel.OnActivate -= cardGameView.Activate;
        cardGameModel.OnDeactivate -= cardGameView.Deactivate;
        cardGameModel.OnChooseIncrease -= cardGameView.ChooseIncrease;
        cardGameModel.OnChooseDecrease -= cardGameView.ChooseDecrease;
        cardGameModel.OnChooseChance_Values -= cardGameView.OnChoose;
        cardGameModel.OnReset -= cardGameView.ResetData;
    }

    #region Input

    public void Activate()
    {
        cardGameModel.Activate();
    }

    public void Deactivate()
    {
        cardGameModel.Deactivate();
    }

    public void Reset()
    {
        cardGameModel.Reset();
    }

    public event Action<bool> OnChooseChance_Values
    {
        add { cardGameModel.OnChooseChance_Values += value; }
        remove { cardGameModel.OnChooseChance_Values -= value; }
    }

    public event Action OnChooseChance
    {
        add { cardGameModel.OnChooseChance += value; }
        remove { cardGameModel.OnChooseChance -= value; }
    }

    #endregion
}
