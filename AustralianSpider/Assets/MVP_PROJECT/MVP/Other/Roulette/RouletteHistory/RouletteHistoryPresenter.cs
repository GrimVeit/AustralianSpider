using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteHistoryPresenter
{
    private RouletteHistoryModel rouletteHistoryModel;
    private RouletteHistoryView rouletteHistoryView;

    public RouletteHistoryPresenter(RouletteHistoryModel rouletteHistoryModel, RouletteHistoryView rouletteHistoryView)
    {
        this.rouletteHistoryModel = rouletteHistoryModel;
        this.rouletteHistoryView = rouletteHistoryView;
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
        rouletteHistoryModel.OnAddRouletteNumberHistory += rouletteHistoryView.AddRouletteNumberHistory;
        rouletteHistoryModel.OnClearHistory += rouletteHistoryView.Clear;
        rouletteHistoryModel.OnLeftScroll += rouletteHistoryView.ScrollLeft;
        rouletteHistoryModel.OnRightScroll += rouletteHistoryView.SctrollRight;
    }

    private void DeactivateEvents()
    {
        rouletteHistoryModel.OnAddRouletteNumberHistory += rouletteHistoryView.AddRouletteNumberHistory;
        rouletteHistoryModel.OnClearHistory += rouletteHistoryView.Clear;
        rouletteHistoryModel.OnLeftScroll += rouletteHistoryView.ScrollLeft;
        rouletteHistoryModel.OnRightScroll += rouletteHistoryView.SctrollRight;
    }

    #region Input

    public void AddRouletteNumber(RouletteSlotValue rouletteSlotValue)
    {
        rouletteHistoryModel.AddRouletteNumber(rouletteSlotValue);
    }

    public void Clear()
    {
        rouletteHistoryModel.Clear();
    }

    #endregion
}
