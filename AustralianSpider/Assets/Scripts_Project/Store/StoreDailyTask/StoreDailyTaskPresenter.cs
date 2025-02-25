using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreDailyTaskPresenter
{
    private readonly StoreDailyTaskModel model;

    public StoreDailyTaskPresenter(StoreDailyTaskModel model)
    {
        this.model = model;
    }

    public void Initalize()
    {
        model.Initialize();
    }

    public void Dispose()
    {
        model.Dispose();
    }

    #region Input

    public event Action<int, int> OnGetYearAndMonth
    {
        add => model.OnGetYearAndMonth += value;
        remove => model.OnGetYearAndMonth -= value;
    }

    public event Action<DayOfWeek> OnGetDayOfWeekFirstDayMonth
    {
        add { model.OnGetDayOfWeekFirstDayMonth += value; }
        remove { model.OnGetDayOfWeekFirstDayMonth -= value; }
    }

    public event Action<DailyTaskData> OnDeselectDailyTask
    {
        add { model.OnDeselectDailyTask += value; }
        remove { model.OnDeselectDailyTask -= value; }
    }

    public event Action<DailyTaskData> OnSelectDailyTask
    {
        add => model.OnSelectDailyTask += value;
        remove => model.OnSelectDailyTask -= value;
    }

    public event Action<DailyTaskData> OnChangeStatusDailyTask
    {
        add => model.OnChangeStatusDailyTask += value;
        remove => model.OnChangeStatusDailyTask -= value;
    }

    public void SelectDailyTask(int id)
    {
        model.SelectDailyTaskData(id);
    }


    public void SetWinStatus()
    {
        model.SetWinStatus();
    }

    public void SetLoseStatus()
    {
        model.SetLoseStatus();
    }

    #endregion
}
