using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemSelectView : View
{
    [SerializeField] private Transform transformSelect;
    [SerializeField] private Transform transformParent;

    public void SelectPack(ShopItemPack itemPack)
    {
        itemPack.SetParent(transformParent);
        itemPack.SelectPack(transformSelect.position, Vector3.one);
    }

    public void UnselectPack(ShopItemPack itemPack)
    {
        itemPack.SetDefaultParent();
        itemPack.UnselectPack();
    }
}
