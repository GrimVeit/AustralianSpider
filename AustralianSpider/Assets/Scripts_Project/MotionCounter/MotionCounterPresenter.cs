using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionCounterPresenter
{
    private MotionCounterModel model;
    private MotionCounterView view;

    public MotionCounterPresenter(MotionCounterModel model, MotionCounterView view)
    {
        this.model = model;
        this.view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        model.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        model.Dispose();
    }

    private void ActivateEvents()
    {
        model.OnChangeMotionCount += view.VisualizeCountMotions;
    }

    private void DeactivateEvents()
    {
        model.OnChangeMotionCount -= view.VisualizeCountMotions;
    }

    #region Input

    public void AddMotion()
    {
        model.AddMotion();
    }

    #endregion
}
