using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCardDesignVisualizePresenter
{
    private readonly FaceCardDesignVisualizeModel model;
    private readonly FaceCardDesignVisualizeView view;

    public FaceCardDesignVisualizePresenter(FaceCardDesignVisualizeModel model, FaceCardDesignVisualizeView view)
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
        model.OnSetFaceCardDesign += view.SetFaceCardDesign;
    }

    private void DeactivateEvents()
    {
        model.OnSetFaceCardDesign -= view.SetFaceCardDesign;
    }

    #region Input

    public void SetFaceCardDesign(FaceCardDesign design)
    {
        model.SetFaceCardDesign(design);
    }

    #endregion
}
