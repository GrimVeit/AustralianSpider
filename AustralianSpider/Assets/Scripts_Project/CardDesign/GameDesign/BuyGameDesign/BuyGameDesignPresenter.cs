using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyGameDesignPresenter
{
    private readonly BuyGameDesignModel model;
    private readonly BuyGameDesignView view;

    public BuyGameDesignPresenter(BuyGameDesignModel model, BuyGameDesignView view)
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
        view.OnBuyGameDesign += model.BuyRandomDesign;
    }

    private void DeactivateEvents()
    {
        view.OnBuyGameDesign -= model.BuyRandomDesign;
    }

    #region Input

    public event Action<int> OnBuyFaceCardDesign
    {
        add { model.OnBuyGameDesign += value; }
        remove { model.OnBuyGameDesign -= value; }
    }

    public void SetOpenGameDesign(GameDesign design)
    {
        model.SetOpenCardDesign(design.ID);
    }

    public void SetCloseGameDesign(GameDesign design)
    {
        model.SetCloseCardDesign(design);
    }

    #endregion
}
