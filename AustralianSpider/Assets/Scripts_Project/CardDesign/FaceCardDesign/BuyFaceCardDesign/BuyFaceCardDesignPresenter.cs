using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyFaceCardDesignPresenter
{
    private readonly BuyFaceCardDesignModel model;
    private readonly BuyFaceCardDesignView view;

    public BuyFaceCardDesignPresenter(BuyFaceCardDesignModel model, BuyFaceCardDesignView view)
    {
        this.model = model;
        this.view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        model.Initialize();
        view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        model.Dispose();
        view.Dispose();
    }

    private void ActivateEvents()
    {
        view.OnBuyFaceCardDesign += model.BuyRandomDesign;
    }

    private void DeactivateEvents()
    {
        view.OnBuyFaceCardDesign -= model.BuyRandomDesign;
    }

    #region Input

    public event Action<int> OnBuyFaceCardDesign
    {
        add { model.OnBuyFaceCardDesign += value; }
        remove { model.OnBuyFaceCardDesign -= value; }
    }

    public void SetOpenCoverCardDesign(FaceCardDesign design)
    {
        model.SetOpenCardDesign(design.ID);
    }

    public void SetCloseCoverCardDesign(FaceCardDesign design)
    {
        model.SetCloseCardDesign(design);
    }

    #endregion
}
