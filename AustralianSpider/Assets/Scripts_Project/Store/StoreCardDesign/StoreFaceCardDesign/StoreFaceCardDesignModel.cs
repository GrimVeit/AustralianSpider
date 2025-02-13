using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StoreFaceCardDesignModel
{
    //public event Action<Ship> OnOpenShip;
    //public event Action<Ship> OnCloseShip;

    //public event Action<Ship> OnSelectOpenShip;
    //public event Action<Ship> OnSelectCloseShip;

    //public event Action<Ship> OnDeselectShip;
    //public event Action<Ship> OnSelectShip;


    //private Ships ships;

    //private Ship currentShip;
    //private ShipData currentGalaxyData;

    //private List<ShipData> shipDatas = new List<ShipData>();

    //public readonly string FilePath = Path.Combine(Application.persistentDataPath, "Ship.json");
    //private IMoneyProvider moneyProvider;

    //public StoreShipModel(Ships ships, IMoneyProvider moneyProvider)
    //{
    //    this.ships = ships;
    //    this.moneyProvider = moneyProvider;
    //}

    //public void Initialize()
    //{
    //    if (File.Exists(FilePath))
    //    {
    //        string loadedJson = File.ReadAllText(FilePath);
    //        ShipDatas shipDatas = JsonUtility.FromJson<ShipDatas>(loadedJson);

    //        Debug.Log("Success");

    //        this.shipDatas = shipDatas.Datas.ToList();
    //    }
    //    else
    //    {
    //        Debug.Log("HDBNJJJJJJJJJJJJJJJJJJJJJJ");

    //        shipDatas = new List<ShipData>();

    //        for (int i = 0; i < 3; i++)
    //        {
    //            if (i == 0)
    //            {
    //                shipDatas.Add(new ShipData(false, true));
    //            }
    //            else
    //            {
    //                shipDatas.Add(new ShipData(false, false));
    //            }
    //        }
    //    }

    //    for (int i = 0; i < ships.ships.Count; i++)
    //    {
    //        ships.ships[i].SetData(shipDatas[i]);

    //        if (shipDatas[i].IsOpen)
    //            OnOpenShip?.Invoke(ships.ships[i]);
    //        else
    //            OnCloseShip?.Invoke(ships.ships[i]);
    //    }

    //    SelectShip(GetSelectShipIndex());
    //}

    //public void Dispose()
    //{
    //    string json = JsonUtility.ToJson(new ShipDatas(shipDatas.ToArray()));
    //    File.WriteAllText(FilePath, json);
    //}

    //public void BuyShip(int number)
    //{
    //    var ship = ships.GetShipByID(number.ToString());

    //    if (ship.ShipData.IsOpen) return;

    //    if (moneyProvider.CanAfford(ship.Price))
    //    {
    //        moneyProvider.SendMoney(-ship.Price);

    //        ship.ShipData.IsOpen = true;

    //        OnOpenShip?.Invoke(ship);

    //        SelectShip(number);
    //    }
    //}

    //public void SelectShip(int number)
    //{
    //    if (currentShip != null)
    //    {
    //        currentShip.ShipData.IsSelect = false;
    //        OnDeselectShip?.Invoke(currentShip);
    //    }

    //    currentShip = ships.GetShipByID(number.ToString());

    //    if (currentShip != null)
    //    {
    //        if (currentShip.ShipData.IsOpen)
    //        {
    //            OnSelectOpenShip?.Invoke(currentShip);
    //        }
    //        else
    //        {
    //            OnSelectCloseShip?.Invoke(currentShip);
    //        }

    //        currentShip.ShipData.IsSelect = true;
    //        OnSelectShip?.Invoke(currentShip);
    //    }
    //}


    //private int GetSelectShipIndex()
    //{
    //    return int.Parse(ships.ships.FirstOrDefault(ship => ship.ShipData.IsSelect == true).GetID());
    //}
}

[Serializable]
public class ShipDatas
{
    public ShipData[] Datas;

    public ShipDatas(ShipData[] datas)
    {
        Datas = datas;
    }
}

[Serializable]
public class ShipData
{
    public bool IsOpen;
    public bool IsSelect;

    public ShipData(bool isOpen, bool isSelect)
    {
        this.IsOpen = isOpen;
        this.IsSelect = isSelect;
    }

    public void Select()
    {
        IsSelect = true;
    }

    public void Open()
    {
        IsOpen = true;
    }
}
