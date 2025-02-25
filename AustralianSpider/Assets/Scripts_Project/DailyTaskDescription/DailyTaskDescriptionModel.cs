using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyTaskDescriptionModel
{
    public event Action<string> OnSetDescription;
    public event Action<int, int> OnSetYearMonth;

    public event Action OnActivatePlay;
    public event Action OnDeactivatePlay;

    public event Action OnPlayDailyTask;

    private readonly DailyTaskDescriptionComments taskDescriptionComments;

    private DailyTaskData currentDailyTaskData;

    public DailyTaskDescriptionModel(DailyTaskDescriptionComments taskDescriptionComments)
    {
        this.taskDescriptionComments = taskDescriptionComments;
    }

    public void SetDailyTaskData(DailyTaskData data)
    {
        currentDailyTaskData = data;

        OnSetDescription?.Invoke(taskDescriptionComments.GetRandomCommentByStatusAndTime(currentDailyTaskData.Status, currentDailyTaskData.TimePeriod));

        if(currentDailyTaskData.Status == DailyTaskStatus.NonePlayed && currentDailyTaskData.TimePeriod == TimePeriod.Present)
        {
            OnActivatePlay?.Invoke();
        }
        else
        {
            OnDeactivatePlay?.Invoke();
        }
    }

    public void SetYearAndMonth(int year, int month)
    {
        OnSetYearMonth?.Invoke(year, month);
    }

    public void PlayDailyTaskGame()
    {
        OnPlayDailyTask?.Invoke();
    }
}
