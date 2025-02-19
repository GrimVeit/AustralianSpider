using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreGameTypePresenter
{
    private readonly StoreGameTypeModel model;

    public StoreGameTypePresenter(StoreGameTypeModel model)
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

    public event Action<GameType> OnDeselectGameType
    {
        add { model.OnDeselectGameType += value; }
        remove { model.OnDeselectGameType -= value; }
    }

    public event Action<GameType> OnSelectGameType
    {
        add { model.OnSelectGameType += value; }
        remove { model.OnSelectGameType -= value; }
    }


    public void SelectGameType(int id)
    {
        model.SelectGameType(id);
    }

    #endregion
}
