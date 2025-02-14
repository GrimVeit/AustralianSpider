using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFaceCardDesignPresenter
{
    private readonly SelectFaceCardDesignModel model;
    private readonly SelectFaceCardDesignView view;

    public SelectFaceCardDesignPresenter(SelectFaceCardDesignModel model, SelectFaceCardDesignView view)
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
        view.OnChooseFaceCardDesign += model.ChooseFaceCardDesign;

        model.OnSetOpenFaceCardDesign += view.SetOpenFaceCardDesign;
        model.OnSetCloseFaceCardDesign += view.SetCloseFaceCardDesign;

        model.OnSelectFaceCardDesign += view.SelectFaceCardDesign;
        model.OnDeselectFaceCardDesign += view.DeselectFaceCardDesign;
    }

    private void DeactivateEvents()
    {
        view.OnChooseFaceCardDesign -= model.ChooseFaceCardDesign;

        model.OnSetOpenFaceCardDesign -= view.SetOpenFaceCardDesign;
        model.OnSetCloseFaceCardDesign -= view.SetCloseFaceCardDesign;

        model.OnSelectFaceCardDesign -= view.SelectFaceCardDesign;
        model.OnDeselectFaceCardDesign -= view.DeselectFaceCardDesign;
    }

    #region Input

    public event Action<int> OnChooseFaceCardDesign
    {
        add { model.OnChooseFaceCardDesign += value; }
        remove { model.OnChooseFaceCardDesign -= value; }
    }



    public void SetOpenFaceCardDesign(FaceCardDesign faceCardDesign)
    {
        model.SetOpenFaceCardDesign(faceCardDesign);
    }

    public void SetCloseFaceCardDesign(FaceCardDesign faceCardDesign)
    {
        model.SetCloseFaceCardDesign(faceCardDesign);
    }



    public void SelectFaceCardDesign(FaceCardDesign design)
    {
        model.SelectFaceCardDesign(design.ID);
    }

    public void DeselectFaceCardDesign(FaceCardDesign design)
    {
        model.DeselectFaceCardDesign(design.ID);
    }


    #endregion
}
