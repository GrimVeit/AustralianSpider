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
        scoreModel.OnChangeAllCountScore += scoreView.DisplayCoins;
    }

    private void DeactivateEvents()
    {
        scoreModel.OnChangeAllCountScore -= scoreView.DisplayCoins;
    }

    #region Input

    public void AddScore(int score = 1)
    {
        scoreModel.AddScore(score);
    }

    public void RemoveHealth(int score = 1)
    {
        scoreModel.RemoveScore(score);
    }

    public void AddScoreByFullComplect()
    {
        scoreModel.AddScore(101);
    }

    public void RemoveScoreByCardDrop()
    {
        scoreModel.RemoveScore(1);
    }

    #endregion
}
