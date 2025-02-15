using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDesignVisualizePresenter
{
    private readonly GameDesignVisualizeModel model;
    private readonly GameDesignVisualizeView view;

    public GameDesignVisualizePresenter(GameDesignVisualizeModel model, GameDesignVisualizeView view)
    {
        this.model = model;
        this.view = view;
    }

    public void Initialize()
    {
        ActivateEvents();
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

    public void SetGameDesign(GameDesign design)
    {
        model.SetGameDesign(design);
    }

    #endregion
}
