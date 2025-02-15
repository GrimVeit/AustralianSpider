using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuyGameDesignModel
{
    public event Action<int> OnBuyGameDesign;

    private List<GameDesign> gameDesigns = new List<GameDesign>();
    private IMoneyProvider moneyProvider;

    private BuyDesignPrices buyDesignPrices;

    private int currentBuyLevel;

    private readonly string KEY;

    private int currentPrice;

    public BuyGameDesignModel(string KEY, BuyDesignPrices buyDesignPrices, IMoneyProvider moneyProvider)
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
        var design = gameDesigns.FirstOrDefault(data => data.ID == id);

        if (design == null) return;

        gameDesigns.Remove(design);
    }

    public void SetCloseCardDesign(GameDesign cardDesign)
    {
        var design = gameDesigns.FirstOrDefault(data => data.ID == cardDesign.ID);

        if (design == null)
        {
            gameDesigns.Add(cardDesign);
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
            OnBuyGameDesign?.Invoke(design.ID);
        }
    }

    private GameDesign GetRandomCoverCardDesign()
    {
        if (gameDesigns.Count == 0) return null;

        return gameDesigns[UnityEngine.Random.Range(0, gameDesigns.Count)];
    }
}
