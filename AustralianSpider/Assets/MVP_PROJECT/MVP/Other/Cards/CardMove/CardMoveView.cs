using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMoveView : View
{
    [SerializeField] private CardDrag cardDrag;
    private Canvas canvas;

    public void Initialize()
    {
        canvas = GetComponentInParent<Canvas>();
        cardDrag.Initialize();
        cardDrag.OnMove += OnMove;
    }

    public void Dispose()
    {
        cardDrag.OnMove -= OnMove;
    }

    public void StartMove()
    {
        cardDrag.StartMove();
    }

    public void Teleport()
    {
        cardDrag.Teleport(Vector3.zero);
    }

    public void EndMove()
    {
        cardDrag.EndMove(Vector3.zero);
    }

    public void Move(Vector2 vector)
    {
        cardDrag.Move(vector);
    }

    #region Input

    private void OnMove(Vector2 vector)
    {
        OnMove_Action?.Invoke(vector / canvas.scaleFactor);
    }

    public event Action<Vector2> OnMove_Action;

    public event Action OnStartMove_Action
    {
        add { cardDrag.OnStartMove += value; }
        remove { cardDrag.OnStartMove -= value; }
    }

    public event Action<PointerEventData> OnEndMove_Action
    {
        add { cardDrag.OnEndMove += value; }
        remove { cardDrag.OnEndMove -= value; }
    }

    public event Action OnStartGrab
    {
        add { cardDrag.OnStartGrab += value; }
        remove { cardDrag.OnStartGrab -= value; }
    }

    #endregion
}
