using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyTaskDescriptionPresenter
{
    private readonly DailyTaskDescriptionModel model;
    private readonly DailyTaskDescriptionView view;

    public DailyTaskDescriptionPresenter(DailyTaskDescriptionModel model, DailyTaskDescriptionView view)
    {
        this.model = model;
        this.view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        view.Dispose();
    }

    private void ActivateEvents()
    {
        view.OnClickToPlayDailyTaask += model.PlayDailyTaskGame;

        model.OnSetYearMonth += view.SetYearMonth;
        model.OnSetDescription += view.SetDescription;
        model.OnActivatePlay += view.ActivatePlay;
        model.OnDeactivatePlay += view.DeactivatePlay;
    }

    private void DeactivateEvents()
    {
        view.OnClickToPlayDailyTaask -= model.PlayDailyTaskGame;

        model.OnSetYearMonth -= view.SetYearMonth;
        model.OnSetDescription -= view.SetDescription;
        model.OnActivatePlay -= view.ActivatePlay;
        model.OnDeactivatePlay -= view.DeactivatePlay;
    }

    #region Input

    public event Action OnPlayDailyTask
    {
        add => model.OnPlayDailyTask += value;
        remove => model.OnPlayDailyTask -= value;
    }

    public void SetYearAndMonth(int year, int month)
    {
        model.SetYearAndMonth(year, month);
    }

    public void SetDailyTaskData(DailyTaskData data)
    {
        model.SetDailyTaskData(data);
    }

    #endregion
}
