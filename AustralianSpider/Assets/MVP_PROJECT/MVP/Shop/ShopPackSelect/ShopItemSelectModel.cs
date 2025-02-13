using System;

public class ShopItemSelectModel
{

    public event Action OnEndSelect;

    public event Action<ShopItemPack> OnSelectPack;
    public event Action<ShopItemPack> OnUnselectPack;
    public event Action OnUnselect;

    private ShopItemPack currentSelectPack;

    private ISoundProvider soundProvider;

    public ShopItemSelectModel(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }


    public void SelectPack(ShopItemPack pack)
    {
        if(currentSelectPack != null)
        {
            currentSelectPack.OnEndSelect -= OnSelectPackMethod;
            OnUnselectPack?.Invoke(currentSelectPack);
            OnUnselect?.Invoke();
        }

        soundProvider.PlayOneShot("NewPack");
        currentSelectPack = pack;
        currentSelectPack.OnEndSelect += OnSelectPackMethod;
        OnSelectPack?.Invoke(currentSelectPack);
    }

    public void Unselect()
    {
        if (currentSelectPack != null)
        {
            OnUnselectPack?.Invoke(currentSelectPack);
            OnUnselect?.Invoke();
        }
    }


    private void OnSelectPackMethod()
    {
        OnEndSelect?.Invoke();
    }
}
