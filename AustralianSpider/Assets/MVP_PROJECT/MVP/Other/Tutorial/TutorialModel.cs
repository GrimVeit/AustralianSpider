using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialModel
{
    public event Action OnDeactivate;

    public event Action<string> OnActivateTutorial;
    public event Action<string> OnDeactivateTutorial;

    private bool isActiveTutorial = true;

    public void ActivateTutorial(string ID)
    {
        if (!isActiveTutorial) return;
        OnActivateTutorial?.Invoke(ID);
    }

    public void DeactivateTutorial(string ID)
    {
        if (!isActiveTutorial) return;
        OnDeactivateTutorial?.Invoke(ID);
    }

    public bool IsActiveTutorial()
    {
        return isActiveTutorial;
    }

    public void Activate()
    {
        isActiveTutorial = true;
    }

    public void Deactivate()
    {
        isActiveTutorial = false;
        OnDeactivate?.Invoke();
    }
}
