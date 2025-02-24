using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectDailyTaskModel
{
    public event Action<DayOfWeek> OnSetDayOfWeakFirstDayMonth;
    public event Action<DailyTaskData> OnSetDailyTaskData;
    public event Action<int> OnSelectDailyTask;
    public event Action<int> OnDeselectDailyTask;

    public event Action<int> OnChooseDailyTask;

    public void SetDayOfWeakFirstDayMonth(DayOfWeek dayOfWeek)
    {
        OnSetDayOfWeakFirstDayMonth?.Invoke(dayOfWeek);
    }

    public void SetDailyTaskData(DailyTaskData data)
    {
        OnSetDailyTaskData?.Invoke(data);
    }

    public void SelectDailyTask(int id)
    {
        OnSelectDailyTask?.Invoke(id);
    }

    public void DeselectDailyTask(int id)
    {
        OnDeselectDailyTask?.Invoke(id);
    }

    public void ChooseDailyTask(int id)
    {
        OnChooseDailyTask?.Invoke(id);
    }
}
