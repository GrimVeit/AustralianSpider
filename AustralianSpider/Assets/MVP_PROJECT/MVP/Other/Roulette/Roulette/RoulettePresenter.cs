using System;
using UnityEngine;

public class RoulettePresenter
{
    private RouletteModel rouletteModel;
    private RouletteView rouletteView;

    public RoulettePresenter(RouletteModel rouletteModel, RouletteView rouletteView)
    {
        this.rouletteModel = rouletteModel;
        this.rouletteView = rouletteView;
    }

    public void Initialize()
    {
        ActivateEvents();

        rouletteView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        rouletteView.Dispose();
    }

    private void ActivateEvents()
    {
        rouletteView.OnStartSpin += rouletteModel.StartSpin;
        rouletteView.OnGetRouletteNumber += rouletteModel.GetRouletteNumber;

        rouletteModel.OnStartSpin += rouletteView.StartSpin;
        rouletteModel.OnRollBallToSlot += rouletteView.RollBallToSlot;
    }

    private void DeactivateEvents()
    {
        rouletteView.OnStartSpin -= rouletteModel.StartSpin;
        rouletteView.OnGetRouletteNumber -= rouletteModel.GetRouletteNumber;

        rouletteModel.OnStartSpin -= rouletteView.StartSpin;
        rouletteModel.OnRollBallToSlot -= rouletteView.RollBallToSlot;
    }

    #region Input

    public void RollBallToSlot(Vector3 vector)
    {
        rouletteModel.RollBallToSlot(vector);
    }

    public event Action<RouletteSlotValue> OnGetRouletteSlotValue
    {
        add { rouletteModel.OnGetRouletteSlotValue += value; }
        remove { rouletteModel.OnGetRouletteSlotValue -= value; }
    }

    #endregion
}
