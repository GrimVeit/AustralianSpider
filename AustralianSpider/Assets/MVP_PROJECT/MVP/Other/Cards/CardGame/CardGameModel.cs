using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameModel
{
    public event Action<bool> OnChooseChance_Values;
    public event Action OnChooseChance;

    public event Action OnActivate;
    public event Action OnDeactivate;

    public event Action OnChooseIncrease;
    public event Action OnChooseDecrease;

    public event Action OnReset;

    private bool chanceIncrease;
    private ITutorialProvider tutorialProvider;
    private ISoundProvider soundProvider;

    public CardGameModel(ITutorialProvider tutorialProvider, ISoundProvider soundProvider)
    {
        this.tutorialProvider = tutorialProvider;
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    public void Activate()
    {
        OnActivate?.Invoke();

        if (tutorialProvider.IsActiveTutorial())
            tutorialProvider.ActivateTutorial("ChooseBet");
    }

    public void Deactivate()
    {
        OnDeactivate?.Invoke();

        if (tutorialProvider.IsActiveTutorial())
            tutorialProvider.DeactivateTutorial("ChooseBet");
    }

    public void Increase()
    {
        chanceIncrease = true;
        soundProvider.PlayOneShot("ChanceIncrease");
        OnChooseIncrease?.Invoke();
    }

    public void Decrease()
    {
        chanceIncrease = false;
        soundProvider.PlayOneShot("ChanceDecrease");
        OnChooseDecrease?.Invoke();
    }

    public void SubmitChance()
    {
        soundProvider.PlayOneShot("ClickOpen");
        OnChooseChance?.Invoke();
        OnChooseChance_Values?.Invoke(chanceIncrease);
    }

    public void Reset()
    {
        OnReset?.Invoke();
    }
}
