using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneVisualizePresenter 
{
    private readonly PhoneVisualizeModel model;
    private readonly PhoneVisualizeView view;

    public PhoneVisualizePresenter(PhoneVisualizeModel model, PhoneVisualizeView view)
    {
        this.model = model;
        this.view = view;
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
        view.OnCompleteMoveFromLandscapeToPortrait += model.CompleteMoveFromLandscapeToPortrait;
        view.OnCompleteMoveFromPortraitToLandscape += model.CompleteMoveFromPortraitToLandscape;

        model.OnActivateMoveFromLandscapeToPortrait += view.LandscapeToPortrait;
        model.OnActivateMoveFromPortraitToLandscape += view.PortraitToLandscape;
    }

    private void DeactivateEvents()
    {
        view.OnCompleteMoveFromLandscapeToPortrait += model.CompleteMoveFromLandscapeToPortrait;
        view.OnCompleteMoveFromPortraitToLandscape += model.CompleteMoveFromPortraitToLandscape;

        model.OnActivateMoveFromLandscapeToPortrait += view.LandscapeToPortrait;
        model.OnActivateMoveFromPortraitToLandscape += view.PortraitToLandscape;
    }

    #region Input

    public event Action OnCompleteMoveFromPortraitToLandscape
    {
        add => model.OnCompleteMoveFromPortraitToLandscape += value;
        remove => model.OnCompleteMoveFromPortraitToLandscape -= value;
    }

    public event Action OnEndMoveFromLandscapeToPortrait
    {
        add => model.OnCompleteMoveFromLandscapeToPortrait += value;
        remove => model.OnCompleteMoveFromLandscapeToPortrait -= value;
    }

    public void LandscapeToPortrait()
    {
        model.LandscapeToPortrait();
    }

    public void PortraitToLandscape()
    {
        model.PortraitToLandscape();
    }

    #endregion
}
