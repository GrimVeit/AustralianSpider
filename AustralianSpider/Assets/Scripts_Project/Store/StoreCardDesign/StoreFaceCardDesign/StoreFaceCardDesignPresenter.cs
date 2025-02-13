using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreFaceCardDesignPresenter
{
    private StoreFaceCardDesignModel model;
    private StoreFaceCardDesignView view;

    public StoreFaceCardDesignPresenter(StoreFaceCardDesignModel model, StoreFaceCardDesignView view)
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

    }

    private void DeactivateEvents()
    {

    }

    #region Input



    #endregion
}
