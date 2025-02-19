using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawnerPresenter
{
    private CardSpawnerModel model;
    private CardSpawnerView view;

    public CardSpawnerPresenter(CardSpawnerModel model, CardSpawnerView view)
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
}
