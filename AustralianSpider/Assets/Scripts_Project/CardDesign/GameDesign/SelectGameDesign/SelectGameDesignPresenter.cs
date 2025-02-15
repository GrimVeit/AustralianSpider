using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGameDesignPresenter
{
    private readonly SelectGameDesignModel model;
    private readonly SelectGameDesignView view;

    public SelectGameDesignPresenter(SelectGameDesignModel model, SelectGameDesignView view)
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
        view.OnChooseGameDesign += model.ChooseGameDesign;

        model.OnSetOpenGameDesign += view.SetOpenCoverCardDesign;
        model.OnSetCloseGameDesign += view.SetCloseCoverCardDesign;

        model.OnSelectGameDesign += view.SelectGameDesign;
        model.OnDeselectGameDesign += view.DeselectGameDesign;
    }

    private void DeactivateEvents()
    {
        view.OnChooseGameDesign -= model.ChooseGameDesign;

        model.OnSetOpenGameDesign -= view.SetOpenCoverCardDesign;
        model.OnSetCloseGameDesign -= view.SetCloseCoverCardDesign;

        model.OnSelectGameDesign -= view.SelectGameDesign;
        model.OnDeselectGameDesign -= view.DeselectGameDesign;
    }

    #region Input

    public event Action<int> OnChooseCoverCardDesign
    {
        add { model.OnChooseGameDesign += value; }
        remove { model.OnChooseGameDesign -= value; }
    }



    public void SetOpenGameDesign(GameDesign coverCardDesign)
    {
        model.SetOpenGameDesign(coverCardDesign);
    }

    public void SetCloseGameDesign(GameDesign coverCardDesign)
    {
        model.SetCloseGameDesign(coverCardDesign);
    }



    public void SelectGameDesign(GameDesign design)
    {
        model.SelectGameDesign(design.ID);
    }

    public void DeselectGameDesign(GameDesign design)
    {
        model.DeselectGameDesign(design.ID);
    }


    #endregion
}
