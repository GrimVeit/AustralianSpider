using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreFaceCardDesignPresenter
{
    private readonly StoreFaceCardDesignModel model;

    public StoreFaceCardDesignPresenter(StoreFaceCardDesignModel model)
    {
        this.model = model;
    }

    public void Initialize()
    {
        model.Initialize();
    }

    public void Dispose()
    {
        model.Dispose();
    }

    #region Input

    public event Action<FaceCardDesign> OnOpenFaceCardDesign
    {
        add { model.OnOpenFaceCardDesign += value; }
        remove { model.OnOpenFaceCardDesign -= value; }
    }

    public event Action<FaceCardDesign> OnCloseFaceCardDesign
    {
        add { model.OnCloseFaceCardDesign += value; }
        remove { model.OnCloseFaceCardDesign -= value; }
    }



    public event Action<FaceCardDesign> OnDeselectFaceCardDesign
    {
        add { model.OnDeselectFaceCardDesign += value; }
        remove { model.OnDeselectFaceCardDesign -= value; }
    }

    public event Action<FaceCardDesign> OnSelectFaceCardDesign
    {
        add { model.OnSelectFaceCardDesign += value; }
        remove { model.OnSelectFaceCardDesign -= value; }
    }


    public void SelectFaceCardDesign(int id)
    {
        model.SelectFaceCardDesign(id);
    }

    public void BuyFaceCardDesign(int id)
    {
        model.BuyFaceCardDesign(id);
    }

    #endregion
}
