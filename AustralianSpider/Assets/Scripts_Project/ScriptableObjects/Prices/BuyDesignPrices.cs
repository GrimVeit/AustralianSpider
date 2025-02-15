using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "DesignPrices", menuName = "Prices/DesignPrices")]
public class BuyDesignPrices : ScriptableObject
{
    public List<BuyDesignPrice> BuyDesignsPrices = new List<BuyDesignPrice>();

    public BuyDesignPrice GetBuyDesignPriceByLevel(int level)
    {
        return BuyDesignsPrices.FirstOrDefault(data => data.Level == level);
    }

    public BuyDesignPrice GetLastDesignPrice()
    {
        return BuyDesignsPrices[^1];
    }
}

[Serializable]
public class BuyDesignPrice
{
    [SerializeField] private int level;
    [SerializeField] private int price;

    public int Level => level;
    public int Price => price;
}
