using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectDailyTaskPresenter
{
    private readonly SelectDailyTaskModel model;
    private readonly SelectDailyTaskView view;

    public SelectDailyTaskPresenter(SelectDailyTaskModel model, SelectDailyTaskView view)
    {
        this.model = model;
        this.view = view;
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
        view.OnChooseDailyTask += model.ChooseDailyTask;

        model.OnSetDayOfWeakFirstDayMonth += view.SetDayOfWeakFirstDayMonth;
        model.OnSetDailyTaskData += view.SetDailyTaskData;
        model.OnSelectDailyTask += view.SelectDailyTask;
        model.OnDeselectDailyTask += view.DeselectDailyTask;
    }

    private void DeactivateEvents()
    {
        view.OnChooseDailyTask -= model.ChooseDailyTask;

        model.OnSetDayOfWeakFirstDayMonth -= view.SetDayOfWeakFirstDayMonth;
        model.OnSetDailyTaskData -= view.SetDailyTaskData;
        model.OnSelectDailyTask -= view.SelectDailyTask;
        model.OnDeselectDailyTask -= view.DeselectDailyTask;
    }

    #region Input

    public event Action<int> OnChooseDailyTask
    {
        add => model.OnChooseDailyTask += value;
        remove => model.OnChooseDailyTask -= value;
    }

    public void SetDayOfWeakFirstDayMonth(DayOfWeek dayOfWeek)
    {
        model.SetDayOfWeakFirstDayMonth(dayOfWeek);
    }

    public void SetDailyTaskData(DailyTaskData data)
    {
        model.SetDailyTaskData(data);
    }

    public void SelectDailyTask(DailyTaskData dailyTaskData)
    {
        model.SelectDailyTask(dailyTaskData.Id);
    }

    public void DeselectDailyTask(DailyTaskData dailyTaskData)
    {
        model.DeselectDailyTask(dailyTaskData.Id);
    }

    #endregion
}
