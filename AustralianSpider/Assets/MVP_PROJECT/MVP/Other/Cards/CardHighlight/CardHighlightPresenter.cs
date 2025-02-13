using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHighlightPresenter
{
    private CardHighlightModel cardHighlightModel;
    private CardHighlightView cardHighlightView;

    public CardHighlightPresenter(CardHighlightModel cardHighlightModel, CardHighlightView cardHighlightView)
    {
        this.cardHighlightModel = cardHighlightModel;
        this.cardHighlightView = cardHighlightView;
    }

    public void Initilize()
    {
        ActivateEvents();

        cardHighlightView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        cardHighlightView.Dispose();
    }

    private void ActivateEvents()
    {
        cardHighlightModel.OnActivateChooseHighlight += cardHighlightView.ActivateChooseHighlight;
        cardHighlightModel.OnDeactivateChooseHighlight += cardHighlightView.DeactivateChooseHighlight;
    }

    private void DeactivateEvents()
    {
        cardHighlightModel.OnActivateChooseHighlight += cardHighlightView.ActivateChooseHighlight;
        cardHighlightModel.OnDeactivateChooseHighlight += cardHighlightView.DeactivateChooseHighlight;
    }

    #region Input

    public void ActivateChooseHighlight()
    {
        cardHighlightModel.ActivateChooseHighlight();
    }

    public void DeactivateChooseHighlight()
    {
        cardHighlightModel.DeactivateChooseHighlight();
    }

    #endregion
}
