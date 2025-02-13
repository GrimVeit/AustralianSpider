using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHighlightModel
{
    public event Action OnActivateChooseHighlight;
    public event Action OnDeactivateChooseHighlight;

    public void ActivateChooseHighlight()
    {
        OnActivateChooseHighlight?.Invoke();
    }

    public void DeactivateChooseHighlight()
    {
        OnDeactivateChooseHighlight?.Invoke();
    }
}
