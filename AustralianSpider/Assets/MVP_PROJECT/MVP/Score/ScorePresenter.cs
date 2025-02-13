using System;

public class ScorePresenter
{
    private ScoreModel scoreModel;
    private ScoreView scoreView;

    public ScorePresenter(ScoreModel scoreModel, ScoreView scoreView)
    {
        this.scoreModel = scoreModel;
        this.scoreView = scoreView;
    }

    public void Initialize()
    {
        ActivateEvents();

        scoreModel.Initialize();
        scoreView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        scoreModel.Dispose();
        scoreView.Dispose();
    }

    private void ActivateEvents()
    {
        scoreModel.OnAddHealth += scoreView.AddHealth;
        scoreModel.OnRemoveHealth += scoreView.RemoveHealth;
        scoreModel.OnChangeAllCountCoins += scoreView.DisplayCoins;
    }

    private void DeactivateEvents()
    {
        scoreModel.OnAddHealth -= scoreView.AddHealth;
        scoreModel.OnRemoveHealth -= scoreView.RemoveHealth;
        scoreModel.OnChangeAllCountCoins -= scoreView.DisplayCoins;
    }

    #region Input

    public event Action OnGameWinned
    {
        add { scoreModel.OnGameWinned += value; }
        remove { scoreModel.OnGameWinned -= value; }
    }

    public event Action OnGameFailed
    {
        add { scoreModel.OnGameFailed += value; }
        remove { scoreModel.OnGameFailed -= value; }
    }

    public void AddScore()
    {
        scoreModel.AddScore();
    }

    public void RemoveHealth()
    {
        scoreModel.RemoveHealth();
    }

    #endregion
}
