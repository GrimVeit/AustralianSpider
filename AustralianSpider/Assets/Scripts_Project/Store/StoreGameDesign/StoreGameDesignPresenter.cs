using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreGameDesignPresenter
{
    private readonly StoreGameDesignModel model;

    public StoreGameDesignPresenter(StoreGameDesignModel model)
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

    public event Action<GameDesign> OnOpenGameDesign
    {
        add { model.OnOpenGameDesign += value; }
        remove { model.OnOpenGameDesign -= value; }
    }

    public event Action<GameDesign> OnCloseGameDesign
    {
        add { model.OnCloseGameDesign += value; }
        remove { model.OnCloseGameDesign -= value; }
    }



    public event Action<GameDesign> OnDeselectGameDesign
    {
        add { model.OnDeselectGameDesign += value; }
        remove { model.OnDeselectGameDesign -= value; }
    }

    public event Action<GameDesign> OnSelectGameDesign
    {
        add { model.OnSelectGameDesign += value; }
        remove { model.OnSelectGameDesign -= value; }
    }


    public void SelectGameDesign(int id)
    {
        model.SelectGameDesign(id);
    }

    public void BuyGameDesign(int id)
    {
        model.BuyGameDesign(id);
    }

    #endregion
}
