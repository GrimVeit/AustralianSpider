using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneVisualizeModel
{
    public event Action OnActivateMoveFromPortraitToLandscape;
    public event Action OnActivateMoveFromLandscapeToPortrait;

    public event Action OnCompleteMoveFromPortraitToLandscape;
    public event Action OnCompleteMoveFromLandscapeToPortrait;

    public void LandscapeToPortrait()
    {
        OnActivateMoveFromLandscapeToPortrait();
    }

    public void PortraitToLandscape()
    {
        OnActivateMoveFromPortraitToLandscape();
    }

    

    public void CompleteMoveFromPortraitToLandscape()
    {
        OnCompleteMoveFromPortraitToLandscape?.Invoke();
    }

    public void CompleteMoveFromLandscapeToPortrait()
    {
        OnCompleteMoveFromLandscapeToPortrait?.Invoke();
    }
}
