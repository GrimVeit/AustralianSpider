using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHistoryPresenter
{
    private CardHistoryModel cardHistoryModel;
    private CardHistoryView cardHistoryView;

    public CardHistoryPresenter(CardHistoryModel cardHistoryModel, CardHistoryView cardHistoryView)
    {
        this.cardHistoryModel = cardHistoryModel;
        this.cardHistoryView = cardHistoryView;
    }

    public void Initialize()
    {
        ActivateEvents();

        cardHistoryView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        cardHistoryView.Dispose();
    }

    private void ActivateEvents()
    {
        cardHistoryView.OnClickToLeftScroll += cardHistoryModel.LeftScroll;
        cardHistoryView.OnClickToRightScroll += cardHistoryModel.RightScroll;

        cardHistoryModel.OnAddCardCombo += cardHistoryView.AddCardCombo;
        cardHistoryModel.OnClearHistory += cardHistoryView.Clear;
        cardHistoryModel.OnLeftScroll += cardHistoryView.ScrollLeft;
        cardHistoryModel.OnRightScroll += cardHistoryView.SctrollRight;
    }

    private void DeactivateEvents()
    {
        cardHistoryView.OnClickToLeftScroll -= cardHistoryModel.LeftScroll;
        cardHistoryView.OnClickToRightScroll -= cardHistoryModel.RightScroll;

        cardHistoryModel.OnAddCardCombo -= cardHistoryView.AddCardCombo;
        cardHistoryModel.OnClearHistory -= cardHistoryView.Clear;
        cardHistoryModel.OnLeftScroll -= cardHistoryView.ScrollLeft;
        cardHistoryModel.OnRightScroll -= cardHistoryView.SctrollRight;
    }

    #region Input

    public void AddCardComboHistory(CardValue leftCard, CardValue rightCard)
    {
        cardHistoryModel.AddCardCombo(leftCard, rightCard);
    }

    public void Clear()
    {
        cardHistoryModel.Clear();
    }

    #endregion
}
