using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverCardDesignVisualizePresenter
{
    private readonly CoverCardDesignVisualizeModel model;
    private readonly CoverCardDesignVisualizeView view;

    public CoverCardDesignVisualizePresenter(CoverCardDesignVisualizeModel model, CoverCardDesignVisualizeView view)
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
        model.OnSetCoverCardDesign += view.SetCoverCardDesign;
    }

    private void DeactivateEvents()
    {
        model.OnSetCoverCardDesign -= view.SetCoverCardDesign;
    }

    #region Input

    public void SetCoverCardDesign(CoverCardDesign design)
    {
        model.SetCoverCardDesign(design);
    }

    #endregion
}
