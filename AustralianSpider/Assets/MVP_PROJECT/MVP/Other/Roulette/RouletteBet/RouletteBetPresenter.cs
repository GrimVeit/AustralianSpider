using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteBetPresenter
{
    private RouletteBetModel rouletteBetModel;
    private RouletteBetView rouletteBetView;

    public RouletteBetPresenter(RouletteBetModel rouletteBetModel, RouletteBetView rouletteBetView)
    {
        this.rouletteBetModel = rouletteBetModel;
        this.rouletteBetView = rouletteBetView;
    }

    public void Initialize()
    {
        ActivateEvents();

        rouletteBetView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        rouletteBetView.Dispose();
    }

    private void ActivateEvents()
    {
        rouletteBetView.OnChooseCell_Action += rouletteBetModel.ChooseCell;
        rouletteBetView.OnResetCell_Action += rouletteBetModel.ResetCell;

        rouletteBetView.OnStartShowResult += rouletteBetModel.StartShowResult;
        rouletteBetView.OnFinishShowResult += rouletteBetModel.FinishShowResult;
        rouletteBetView.OnStartHideResult += rouletteBetModel.StartHideResult;
        rouletteBetView.OnFinishHideResult += rouletteBetModel.FinishHideResult;

        rouletteBetModel.OnChangeBet += rouletteBetView.BetDisplay;
        rouletteBetModel.OnChangeBetCount += rouletteBetView.BetCountDisplay;
        rouletteBetModel.OnChangeWin += rouletteBetView.WinDisplay;
        rouletteBetModel.OnChangeProfit += rouletteBetView.ProfitDisplay;

        rouletteBetModel.OnShowResult += rouletteBetView.ShowResult;
        rouletteBetModel.OnHideResult += rouletteBetView.HideResult;
    }

    private void DeactivateEvents()
    {
        rouletteBetView.OnChooseCell_Action -= rouletteBetModel.ChooseCell;
        rouletteBetView.OnResetCell_Action -= rouletteBetModel.ResetCell;

        rouletteBetView.OnStartShowResult -= rouletteBetModel.StartShowResult;
        rouletteBetView.OnFinishShowResult -= rouletteBetModel.FinishShowResult;
        rouletteBetView.OnStartHideResult -= rouletteBetModel.StartHideResult;
        rouletteBetView.OnFinishHideResult -= rouletteBetModel.FinishHideResult;

        rouletteBetModel.OnChangeBet -= rouletteBetView.BetDisplay;
        rouletteBetModel.OnChangeBetCount -= rouletteBetView.BetCountDisplay;
        rouletteBetModel.OnChangeWin -= rouletteBetView.WinDisplay;
        rouletteBetModel.OnChangeProfit -= rouletteBetView.ProfitDisplay;

        rouletteBetModel.OnShowResult -= rouletteBetView.ShowResult;
        rouletteBetModel.OnHideResult -= rouletteBetView.HideResult;
    }

    #region Input

    public void ReturnChips()
    {
        rouletteBetModel.ReturnChips();
    }

    public void ShowResult()
    {
        rouletteBetModel.ShowResult();
    }

    public void HideResult()
    {
        rouletteBetModel.HideResult();
    }

    public void GetRouletteSlotValue(RouletteSlotValue rouletteSlotValue)
    {
        rouletteBetModel.GetRouletteSlotValue(rouletteSlotValue);
    }

    public void SearchWin()
    {
        rouletteBetModel.SearchWin();
    }


    public event Action OnStartShowResult
    {
        add { rouletteBetModel.OnStartShowResult += value; }
        remove { rouletteBetModel.OnStartShowResult -= value; }
    }

    public event Action OnFinishShowResult
    {
        add { rouletteBetModel.OnFinishShowResult += value; }
        remove { rouletteBetModel.OnFinishShowResult -= value; }
    }

    public event Action OnStartHideResult
    {
        add { rouletteBetModel.OnStartHideResult += value; }
        remove { rouletteBetModel.OnStartHideResult -= value; }
    }

    public event Action OnFinishHideResult
    {
        add { rouletteBetModel.OnFinishHideResult += value; }
        remove { rouletteBetModel.OnFinishHideResult -= value; }
    }



    public event Action<Chip> OnNoneRetractedChip
    {
        add { rouletteBetModel.OnNoneRetractedChip += value; }
        remove { rouletteBetModel.OnNoneRetractedChip -= value; }
    }

    public event Action<Chip> OnDestroyedChip
    {
        add { rouletteBetModel.OnDestroyedChip += value; }
        remove { rouletteBetModel.OnDestroyedChip -= value; }
    }

    #endregion
}
