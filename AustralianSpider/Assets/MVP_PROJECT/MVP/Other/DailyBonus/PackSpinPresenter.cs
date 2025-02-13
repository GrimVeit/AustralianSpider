using System;

public class PackSpinPresenter
{
    private PackSpinModel model;
    private PackSpinView view;

    public PackSpinPresenter(PackSpinModel model, PackSpinView view)
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
        view.OnClickSpinButton += model.Spin;
        view.OnSpin += model.OnSpin;
        view.OnGetShopItemPack += model.GetShopItemPack;

        model.OnActivateSpin += view.StartSpin;
        model.OnUnvailableBonusButton += view.DeactivateSpinButton;
        model.OnAvailableBonusButton += view.ActivateSpinButton;
    }

    private void DeactivateEvents()
    {
        view.OnClickSpinButton -= model.Spin;
        view.OnSpin -= model.OnSpin;
        view.OnGetShopItemPack -= model.GetShopItemPack;

        model.OnActivateSpin -= view.StartSpin;
        model.OnUnvailableBonusButton -= view.DeactivateSpinButton;
        model.OnAvailableBonusButton -= view.ActivateSpinButton;
    }

    #region

    public void SetAvailable()
    {
        model.SetAvailable();
    }

    public void SetUnvailable()
    {
        model.SetUnvailable();
    }

    public void Spin()
    {
        model.Spin();
    }

    public event Action<Pack> OnGetPack_DataPack
    {
        add { model.OnGetPack_DataPack += value; }
        remove { model.OnGetPack_DataPack -= value; }
    }

    public event Action<ShopItemPack> OnGetPack_Pack
    {
        add { model.OnGetPack_Pack += value; }
        remove { model.OnGetPack_Pack -= value; }
    }

    public event Action OnActivateSpin
    {
        add { model.OnActivateSpin += value; }
        remove { model.OnActivateSpin -= value; }
    }

    #endregion
}
