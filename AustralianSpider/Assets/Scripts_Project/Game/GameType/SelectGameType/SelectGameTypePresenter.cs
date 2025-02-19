using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGameTypePresenter
{
    private readonly SelectGameTypeModel model;
    private readonly SelectGameTypeView view;

    public SelectGameTypePresenter(SelectGameTypeModel model, SelectGameTypeView view)
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
        view.OnChooseGameType += model.ChooseGameType;

        model.OnSelectGameType += view.SelectGameType;
        model.OnDeselectGameType += view.DeselectGameType;
    }

    private void DeactivateEvents()
    {
        view.OnChooseGameType -= model.ChooseGameType;

        model.OnSelectGameType -= view.SelectGameType;
        model.OnDeselectGameType -= view.DeselectGameType;
    }

    #region Input

    public event Action<int> OnChooseGameType
    {
        add { model.OnChooseGameType += value; }
        remove { model.OnChooseGameType -= value; }
    }



    public void SelectGameType(GameType design)
    {
        model.SelectGameType(design.ID);
    }

    public void DeselectGameType(GameType design)
    {
        model.DeselectGameType(design.ID);
    }


    #endregion
}
