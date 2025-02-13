using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteDesignPresenter
{
    private RouletteDesignModel rouletteDesignModel;
    private RouletteDesignView rouletteDesignView;

    public RouletteDesignPresenter(RouletteDesignModel rouletteDesignModel, RouletteDesignView rouletteDesignView)
    {
        this.rouletteDesignModel = rouletteDesignModel;
        this.rouletteDesignView = rouletteDesignView;
    }

    public void Initialize()
    {
        ActivateEvents();

        rouletteDesignModel.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        rouletteDesignModel.Dispose();
    }

    private void ActivateEvents()
    {
        rouletteDesignModel.OnChooseIndexDesign += rouletteDesignView.ChooseDesign;
    }

    private void DeactivateEvents()
    {
        rouletteDesignModel.OnChooseIndexDesign -= rouletteDesignView.ChooseDesign;
    }
}
