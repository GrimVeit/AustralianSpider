using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionHintPresenter
{
    private readonly MotionHintModel model;
    private readonly MotionHintView view;

    public MotionHintPresenter(MotionHintModel model, MotionHintView view)
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
        view.OnClickToHint += model.MotionHint;
    }

    private void DeactivateEvents()
    {
        view.OnClickToHint -= model.MotionHint;
    }

    #region Input

    public event Action OnMotionHint
    {
        add { model.OnMotionHint += value; }
        remove { model.OnMotionHint -= value; }
    }

    #endregion
}
