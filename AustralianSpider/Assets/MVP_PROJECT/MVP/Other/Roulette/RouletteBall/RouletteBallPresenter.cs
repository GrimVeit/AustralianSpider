
using System;
using UnityEngine;

public class RouletteBallPresenter
{
    private RouletteBallModel rouletteBallModel;
    private RouletteBallView rouletteBallView;

    public RouletteBallPresenter(RouletteBallModel rouletteBallModel, RouletteBallView rouletteBallView)
    {
        this.rouletteBallModel = rouletteBallModel;
        this.rouletteBallView = rouletteBallView;
    }

    public void Initialize()
    {
        ActivateEvents();

        rouletteBallView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        rouletteBallView.Dispose();
    }

    private void ActivateEvents()
    {
        rouletteBallView.OnClickToSpinButton += rouletteBallModel.StartSpin;
        rouletteBallView.OnBallStopped += rouletteBallModel.BallStopped;

        rouletteBallModel.OnStartSpin += rouletteBallView.StartSpin;
    }

    private void DeactivateEvents()
    {
        rouletteBallView.OnClickToSpinButton -= rouletteBallModel.StartSpin;
        rouletteBallView.OnBallStopped -= rouletteBallModel.BallStopped;

        rouletteBallModel.OnStartSpin -= rouletteBallView.StartSpin;
    }

    #region Input

    public event Action<Vector3> OnBallStopped
    {
        add { rouletteBallModel.OnBallStopped += value; }
        remove { rouletteBallModel.OnBallStopped -= value; }
    }

    #endregion
}
