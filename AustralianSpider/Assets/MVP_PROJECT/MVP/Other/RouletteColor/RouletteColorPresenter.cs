using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteColorPresenter
{
    private RouletteColorModel rouletteColorModel;
    private RouletteColorView rouletteColorView;

    public RouletteColorPresenter(RouletteColorModel rouletteColorModel, RouletteColorView rouletteColorView)
    {
        this.rouletteColorModel = rouletteColorModel;
        this.rouletteColorView = rouletteColorView;
    }

    public void Initialize()
    {
        ActivateEvents();

        rouletteColorModel.Initialize();
        rouletteColorView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        rouletteColorModel.Dispose();
        rouletteColorView.Dispose();
    }

    private void ActivateEvents()
    {
        rouletteColorView.OnChooseColorIndex += rouletteColorModel.ChooseColorIndex;

        rouletteColorModel.OnChooseColorIndex += rouletteColorView.ChooseColorIndex;
    }

    private void DeactivateEvents()
    {
        rouletteColorView.OnChooseColorIndex -= rouletteColorModel.ChooseColorIndex;

        rouletteColorModel.OnChooseColorIndex -= rouletteColorView.ChooseColorIndex;
    }
}
