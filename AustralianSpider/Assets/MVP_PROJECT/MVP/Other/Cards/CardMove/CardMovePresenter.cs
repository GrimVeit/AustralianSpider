using System;

public class CardMovePresenter
{
    private CardMoveModel cardDraggingModel;
    private CardMoveView cardDraggingView;

    public CardMovePresenter(CardMoveModel cardDraggingModel, CardMoveView cardDraggingView)
    {
        this.cardDraggingModel = cardDraggingModel;
        this.cardDraggingView = cardDraggingView;
    }

    public void Initialize()
    {
        ActivateEvents();

        cardDraggingView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        cardDraggingView.Dispose();
    }

    private void ActivateEvents()
    {
        cardDraggingView.OnStartMove_Action += cardDraggingModel.StartMove;
        cardDraggingView.OnEndMove_Action += cardDraggingModel.EndMove;
        cardDraggingView.OnMove_Action += cardDraggingModel.Move;
        cardDraggingView.OnStartGrab += cardDraggingModel.StartGrab;

        cardDraggingModel.OnMove += cardDraggingView.Move;
        cardDraggingModel.OnStartMove += cardDraggingView.StartMove;
        cardDraggingModel.OnEndMove += cardDraggingView.EndMove;

        cardDraggingModel.OnTeleporting += cardDraggingView.Teleport;
    }

    private void DeactivateEvents()
    {
        cardDraggingView.OnStartMove_Action -= cardDraggingModel.StartMove;
        cardDraggingView.OnEndMove_Action -= cardDraggingModel.EndMove;
        cardDraggingView.OnMove_Action -= cardDraggingModel.Move;
        cardDraggingView.OnStartGrab -= cardDraggingModel.StartGrab;

        cardDraggingModel.OnMove -= cardDraggingView.Move;
        cardDraggingModel.OnStartMove -= cardDraggingView.StartMove;
        cardDraggingModel.OnEndMove -= cardDraggingView.EndMove;

        cardDraggingModel.OnTeleporting -= cardDraggingView.Teleport;
    }

    #region Input

    public event Action OnStartMove
    {
        add { cardDraggingModel.OnStartMove += value; }
        remove { cardDraggingModel.OnStartMove -= value; }
    }

    public event Action OnStopMove
    {
        add { cardDraggingModel.OnEndMove += value; }
        remove { cardDraggingModel.OnEndMove -= value; }
    }

    public void TeleportBack()
    {
        cardDraggingModel.Teleport();
    }

    public void Activate()
    {
        cardDraggingModel.Activate();
    }

    public void Deactivate()
    {
        cardDraggingModel.Deactivate();
    }

    #endregion
}
