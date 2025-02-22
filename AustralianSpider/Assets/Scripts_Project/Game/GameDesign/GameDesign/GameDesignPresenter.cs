using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDesignPresenter
{
    private readonly GameDesignModel model;
    private readonly GameDesignView view;

    public GameDesignPresenter(GameDesignModel model, GameDesignView view)
    {
        this.model = model;
        this.view = view;

        ActivateEvents();
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        model.OnSetGameDesign += view.SetGameDesign;
    }

    private void DeactivateEvents()
    {
        model.OnSetGameDesign -= view.SetGameDesign;
    }

    #region Input

    public void SetGameDesign(GameDesign gameDesign)
    {
        model.SetGameDesign(gameDesign);
    }

    #endregion
}
