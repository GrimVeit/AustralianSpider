using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuyCoverCardDesignModel
{
    public event Action<int> OnBuyCoverCardDesign;

    private List<CoverCardDesign> coverCardDesigns = new List<CoverCardDesign>();
    private IMoneyProvider moneyProvider;

    private BuyDesignPrices buyDesignPrices;

    private int currentBuyLevel;

    private readonly string KEY;

    private int currentPrice;

    public BuyCoverCardDesignModel(string KEY, BuyDesignPrices buyDesignPrices, IMoneyProvider moneyProvider)
    {
        this.moneyProvider = moneyProvider;
        this.KEY = KEY;
        this.buyDesignPrices = buyDesignPrices;
    }

    public void Initialize()
    {
        currentBuyLevel = PlayerPrefs.GetInt(KEY, 0);
    }

    public void Dispose()
    {
        PlayerPrefs.SetInt(KEY, currentBuyLevel);
    }

    public void SetOpenCardDesign(int id)
    {
        var design = coverCardDesigns.FirstOrDefault(data => data.ID == id);

        if(design == null) return;

        coverCardDesigns.Remove(design);
    }

    public void SetCloseCardDesign(CoverCardDesign cardDesign)
    {
        var design = coverCardDesigns.FirstOrDefault(data => data.ID == cardDesign.ID);

        if(design == null)
        {
            coverCardDesigns.Add(cardDesign);
        }
    }

    public void BuyRandomDesign()
    {
        var design = GetRandomCoverCardDesign();
        if(design == null) return;


        var buyDesignPrice = buyDesignPrices.GetBuyDesignPriceByLevel(currentBuyLevel);
        if(buyDesignPrice == null)
        {
            buyDesignPrice = buyDesignPrices.GetLastDesignPrice();
        }

        currentPrice = buyDesignPrice.Price;

        if (moneyProvider.CanAfford(currentPrice))
        {
            Debug.Log($"÷≈Õ¿ - {currentPrice}");
            moneyProvider.SendMoney(-currentPrice);
            currentBuyLevel += 1;
            OnBuyCoverCardDesign?.Invoke(design.ID);
        }
    }

    private CoverCardDesign GetRandomCoverCardDesign()
    {
        if(coverCardDesigns.Count == 0) return null;

        return coverCardDesigns[UnityEngine.Random.Range(0, coverCardDesigns.Count)];
    }
}
