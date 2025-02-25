using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyTaskDescriptionModel
{
    public event Action<string> OnSetDescription;

    private DailyTaskDescriptionComments taskDescriptionComments;

    public DailyTaskDescriptionModel(DailyTaskDescriptionComments taskDescriptionComments)
    {
        this.taskDescriptionComments = taskDescriptionComments;
    }

    public void SetDailyTaskData(DailyTaskData data)
    {
        OnSetDescription?.Invoke(taskDescriptionComments.GetRandomCommentByStatusAndTime(data.Status, data.TimePeriod));
    }
}
