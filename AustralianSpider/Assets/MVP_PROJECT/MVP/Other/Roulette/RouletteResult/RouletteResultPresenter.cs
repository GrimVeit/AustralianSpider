using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteResultPresenter
{
    private RouletteResultModel rouletteResultModel;
    private RouletteResultView rouletteResultView;

    public RouletteResultPresenter(RouletteResultModel rouletteResultModel, RouletteResultView rouletteResultView)
    {
        this.rouletteResultModel = rouletteResultModel;
        this.rouletteResultView = rouletteResultView;
    }

    public void Initialize()
    {
        ActivateEvents();

        rouletteResultView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        rouletteResultView.Dispose();
    }

    private void ActivateEvents()
    {
        rouletteResultView.OnStartShowResult += rouletteResultModel.StartShowResult;
        rouletteResultView.OnFinishShowResult += rouletteResultModel.FinishShowResult;
        rouletteResultView.OnStartHideResult += rouletteResultModel.StartHideResult;
        rouletteResultView.OnFinishHideResult += rouletteResultModel.FinishHideResult;

        rouletteResultModel.OnShowResult += rouletteResultView.ShowResult;
        rouletteResultModel.OnHideResult += rouletteResultView.HideResult;
    }

    private void DeactivateEvents()
    {
        rouletteResultView.OnStartShowResult -= rouletteResultModel.StartShowResult;
        rouletteResultView.OnFinishShowResult -= rouletteResultModel.FinishShowResult;
        rouletteResultView.OnStartHideResult -= rouletteResultModel.StartHideResult;
        rouletteResultView.OnFinishHideResult -= rouletteResultModel.FinishHideResult;

        rouletteResultModel.OnShowResult -= rouletteResultView.ShowResult;
        rouletteResultModel.OnHideResult -= rouletteResultView.HideResult;
    }

    #region Input

    public void ShowResult(RouletteSlotValue rouletteSlotValue)
    {
        rouletteResultModel.ShowResult(rouletteSlotValue);
    }

    public void HideResult()
    {
        rouletteResultModel.HideResult();
    }

    public event Action OnStartShowResult
    {
        add { rouletteResultModel.OnStartShowResult += value; }
        remove { rouletteResultModel.OnStartShowResult -= value; }
    }

    public event Action OnFinishShowResult
    {
        add { rouletteResultModel.OnFinishShowResult += value; }
        remove { rouletteResultModel.OnFinishShowResult -= value; }
    }

    public event Action<RouletteSlotValue> OnStartHideResult
    {
        add { rouletteResultModel.OnStartHideResult += value; }
        remove { rouletteResultModel.OnStartHideResult -= value; }
    }

    public event Action OnFinishHideResult
    {
        add { rouletteResultModel.OnFinishHideResult += value; }
        remove { rouletteResultModel.OnFinishHideResult -= value; }
    }

    #endregion
}
