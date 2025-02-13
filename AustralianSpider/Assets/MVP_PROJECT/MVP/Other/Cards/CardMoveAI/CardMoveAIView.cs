using System;
using UnityEngine;

public class CardMoveAIView : View
{
    public event Action OnEndMove;

    [SerializeField] private Transform moveToTransform;
    [SerializeField] private CardMoveAI cardMoveAI;

    public void Initialize()
    {
        cardMoveAI.OnEndMove += HandlerOnEndMove;

        cardMoveAI.Initialize();
    }

    public void Dispose()
    {
        cardMoveAI.OnEndMove -= HandlerOnEndMove;

        cardMoveAI.Dispose();
    }

    public void StartMove()
    {
        cardMoveAI.StartMove(moveToTransform.position, 0.7f);
    }

    public void Teleport()
    {
        cardMoveAI.Teleport(Vector3.zero);
    }

    private void HandlerOnEndMove()
    {
        OnEndMove?.Invoke();
    }
}
