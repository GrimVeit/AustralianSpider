using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCoverCardDesignPresenter
{
    private readonly SelectCoverCardDesignModel model;
    private readonly SelectCoverCardDesignView view;

    public SelectCoverCardDesignPresenter(SelectCoverCardDesignModel model, SelectCoverCardDesignView view)
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
        view.OnChooseCoverCardDesign += model.ChooseFaceCardDesign;

        model.OnSetOpenCoverCardDesign += view.SetOpenCoverCardDesign;
        model.OnSetCloseCoverCardDesign += view.SetCloseCoverCardDesign;

        model.OnSelectCoverCardDesign += view.SelectCoverCardDesign;
        model.OnDeselectCoverCardDesign += view.DeselectCoverCardDesign;
    }

    private void DeactivateEvents()
    {
        view.OnChooseCoverCardDesign -= model.ChooseFaceCardDesign;

        model.OnSetOpenCoverCardDesign -= view.SetOpenCoverCardDesign;
        model.OnSetCloseCoverCardDesign -= view.SetCloseCoverCardDesign;

        model.OnSelectCoverCardDesign -= view.SelectCoverCardDesign;
        model.OnDeselectCoverCardDesign -= view.DeselectCoverCardDesign;
    }

    #region Input

    public event Action<int> OnChooseCoverCardDesign
    {
        add { model.OnChooseCoverCardDesign += value; }
        remove { model.OnChooseCoverCardDesign -= value; }
    }



    public void SetOpenCoverCardDesign(CoverCardDesign coverCardDesign)
    {
        model.SetOpenCoverCardDesign(coverCardDesign);
    }

    public void SetCloseCoverCardDesign(CoverCardDesign coverCardDesign)
    {
        model.SetCloseFaceCardDesign(coverCardDesign);
    }



    public void SelectCoverCardDesign(CoverCardDesign design)
    {
        model.SelectCoverCardDesign(design.ID);
    }

    public void DeselectCoverCardDesign(CoverCardDesign design)
    {
        model.DeselectFaceCardDesign(design.ID);
    }


    #endregion
}
