using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPresenter : ITutorialProvider
{
    private TutorialModel tutorialModel;
    private TutorialView tutorialView;

    public TutorialPresenter(TutorialModel tutuorialModel, TutorialView tutorialView)
    {
        this.tutorialModel = tutuorialModel;
        this.tutorialView = tutorialView;
    }

    public void Initialize()
    {
        ActivateEvents();
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        tutorialModel.OnActivateTutorial += tutorialView.ActivateTutorial;
        tutorialModel.OnDeactivateTutorial += tutorialView.DeactivateTutorial;
        tutorialModel.OnDeactivate += tutorialView.AllDeactivates;
    }

    private void DeactivateEvents()
    {
        tutorialModel.OnActivateTutorial -= tutorialView.ActivateTutorial;
        tutorialModel.OnDeactivateTutorial -= tutorialView.DeactivateTutorial;
        tutorialModel.OnDeactivate -= tutorialView.AllDeactivates;
    }

    #region Input

    public void Activate()
    {
        tutorialModel.Activate();
    }

    public void Deactivate()
    {
        tutorialModel.Deactivate();
    }

    public void ActivateTutorial(string ID)
    {
        tutorialModel.ActivateTutorial(ID);
    }

    public void DeactivateTutorial(string ID)
    {
        tutorialModel.DeactivateTutorial(ID);
    }

    public bool IsActiveTutorial()
    {
        return tutorialModel.IsActiveTutorial();
    }

    #endregion
}

public interface ITutorialProvider
{
    void Activate();
    void Deactivate();
    void ActivateTutorial(string ID);
    void DeactivateTutorial(string ID);
    bool IsActiveTutorial();
}
