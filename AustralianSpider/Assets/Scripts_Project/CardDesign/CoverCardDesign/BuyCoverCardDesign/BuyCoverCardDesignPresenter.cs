using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyCoverCardDesignPresenter
{
    private readonly BuyCoverCardDesignModel model;
    private readonly BuyCoverCardDesignView view;

    public BuyCoverCardDesignPresenter(BuyCoverCardDesignModel model, BuyCoverCardDesignView view)
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
        view.OnBuyCoverCardDesign += model.BuyRandomDesign;
    }

    private void DeactivateEvents()
    {
        view.OnBuyCoverCardDesign -= model.BuyRandomDesign;
    }

    #region Input

    public event Action<int> OnBuyCoverCardDesign
    {
        add { model.OnBuyCoverCardDesign += value; }
        remove { model.OnBuyCoverCardDesign -= value; }
    }

    public void SetOpenCoverCardDesign(CoverCardDesign design)
    {
        model.SetOpenCardDesign(design.ID);
    }

    public void SetCloseCoverCardDesign(CoverCardDesign design)
    {
        model.SetCloseCardDesign(design);
    }

    #endregion
}
