using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMoveAIPresenter
{
    private CardMoveAIModel cardMoveAIModel;
    private CardMoveAIView cardMoveAIView;

    public CardMoveAIPresenter(CardMoveAIModel cardMoveAIModel, CardMoveAIView cardMoveAIView)
    {
        this.cardMoveAIModel = cardMoveAIModel;
        this.cardMoveAIView = cardMoveAIView;
    }

    public void Initialize()
    {
        ActivateEvents();

        cardMoveAIView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        cardMoveAIView.Dispose();
    }

    private void ActivateEvents()
    {
        cardMoveAIView.OnEndMove += cardMoveAIModel.EndMove;

        cardMoveAIModel.OnStartMove += cardMoveAIView.StartMove;
        cardMoveAIModel.OnEndMove += cardMoveAIView.Teleport;
    }

    private void DeactivateEvents()
    {
        cardMoveAIView.OnEndMove -= cardMoveAIModel.EndMove;

        cardMoveAIModel.OnStartMove -= cardMoveAIView.StartMove;
        cardMoveAIModel.OnEndMove -= cardMoveAIView.Teleport;
    }

    #region

    public void Activate()
    {
        cardMoveAIModel.Activate();
    }

    public void Deactivate()
    {
        cardMoveAIModel.Deactivate();
    }

    public event Action OnStartMove
    {
        add { cardMoveAIModel.OnStartMove += value; }
        remove { cardMoveAIModel.OnStartMove -= value; }
    }

    public event Action OnEndMove
    {
        add { cardMoveAIModel.OnEndMove += value; }
        remove { cardMoveAIModel.OnEndMove -= value; }
    }

    #endregion
}
