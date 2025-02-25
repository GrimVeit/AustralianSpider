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
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        model.OnSetDescription += view.SetDescription;
    }

    private void DeactivateEvents()
    {
        model.OnSetDescription -= view.SetDescription;
    }

    #region Input

    public void SetDailyTaskData(DailyTaskData data)
    {
        model.SetDailyTaskData(data);
    }

    #endregion
}
