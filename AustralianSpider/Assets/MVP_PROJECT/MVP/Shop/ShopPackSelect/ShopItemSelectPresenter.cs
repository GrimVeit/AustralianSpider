using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemSelectPresenter
{
    private ShopItemSelectModel model;
    private ShopItemSelectView view;

    public ShopItemSelectPresenter(ShopItemSelectModel model, ShopItemSelectView view)
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
        model.OnSelectPack += view.SelectPack;
        model.OnUnselectPack += view.UnselectPack;
    }

    private void DeactivateEvents()
    {
        model.OnSelectPack -= view.SelectPack;
        model.OnUnselectPack -= view.UnselectPack;
    }

    #region Input

    public event Action OnEndSelect
    {
        add { model.OnEndSelect += value; }
        remove { model.OnEndSelect -= value; }
    }

   
    public void Unselect()
    {
        model.Unselect();
    }

    public void SelectPack(ShopItemPack shopItemPack)
    {
        model.SelectPack(shopItemPack);
    }

    #endregion
}
