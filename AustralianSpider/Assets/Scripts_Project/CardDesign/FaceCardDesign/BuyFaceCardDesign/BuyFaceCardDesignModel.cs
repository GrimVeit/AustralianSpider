using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuyFaceCardDesignModel
{
    public event Action<int> OnBuyFaceCardDesign;

    private List<FaceCardDesign> faceCardDesigns = new List<FaceCardDesign>();
    private IMoneyProvider moneyProvider;

    private BuyDesignPrices buyDesignPrices;

    private int currentBuyLevel;

    private readonly string KEY;

    private int currentPrice;

    public BuyFaceCardDesignModel(string KEY, BuyDesignPrices buyDesignPrices, IMoneyProvider moneyProvider)
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
        var design = faceCardDesigns.FirstOrDefault(data => data.ID == id);

        if (design == null) return;

        faceCardDesigns.Remove(design);
    }

    public void SetCloseCardDesign(FaceCardDesign cardDesign)
    {
        var design = faceCardDesigns.FirstOrDefault(data => data.ID == cardDesign.ID);

        if (design == null)
        {
            faceCardDesigns.Add(cardDesign);
        }
    }

    public void BuyRandomDesign()
    {
        var design = GetRandomCoverCardDesign();
        if (design == null) return;


        var buyDesignPrice = buyDesignPrices.GetBuyDesignPriceByLevel(currentBuyLevel);
        if (buyDesignPrice == null)
        {
            buyDesignPrice = buyDesignPrices.GetLastDesignPrice();
        }

        currentPrice = buyDesignPrice.Price;

        if (moneyProvider.CanAfford(currentPrice))
        {
            Debug.Log($"÷≈Õ¿ - {currentPrice}");
            moneyProvider.SendMoney(-currentPrice);
            currentBuyLevel += 1;
            OnBuyFaceCardDesign?.Invoke(design.ID);
        }
    }

    private FaceCardDesign GetRandomCoverCardDesign()
    {
        if (faceCardDesigns.Count == 0) return null;

        return faceCardDesigns[UnityEngine.Random.Range(0, faceCardDesigns.Count)];
    }
}
