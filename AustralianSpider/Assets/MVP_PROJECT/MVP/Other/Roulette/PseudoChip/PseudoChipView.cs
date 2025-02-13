using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PseudoChipView : View
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private List<PseudoChip> pseudoChips = new List<PseudoChip>();

    [SerializeField] private PseudoChip currentPseudoChip;

    public void Initialize()
    {
        for (int i = 0; i < pseudoChips.Count; i++)
        {
            pseudoChips[i].OnGrabbing += OnGrabPseudoChip;
            pseudoChips[i].Initialize();
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < pseudoChips.Count; i++)
        {
            pseudoChips[i].OnGrabbing -= OnGrabPseudoChip;
            pseudoChips[i].Dispose();
        }
    }

    public void GrabPseudoChip(PseudoChip chip)
    {
        UngrabCurrentPseudoChip();

        currentPseudoChip = chip;

        currentPseudoChip.OnStartMove += OnStartMove;
        currentPseudoChip.OnMove += OnMove;
        currentPseudoChip.OnEndMove += OnEndMove;
    }

    public void UngrabCurrentPseudoChip()
    {
        if (currentPseudoChip != null)
        {
            currentPseudoChip.OnStartMove -= OnStartMove;
            currentPseudoChip.OnMove -= OnMove;
            currentPseudoChip.OnEndMove -= OnEndMove;

            Teleport();
        }
    }

    public void Teleport()
    {
        currentPseudoChip.Teleport();
    }

    public void StartMove()
    {
        currentPseudoChip.StartMove();
    }

    public void EndMove()
    {
        currentPseudoChip.EndMove();
    }

    public void Move(Vector2 vector)
    {
        currentPseudoChip.Move(vector);
    }

    #region Input

    public void OnGrabPseudoChip(PseudoChip pseudoChip)
    {
        OnGrabPseudoChip_Action?.Invoke(pseudoChip);
    }

    private void OnMove(Vector2 vector)
    {
        OnMove_Action?.Invoke(vector / canvas.scaleFactor);
    }

    private void OnStartMove()
    {
        OnStartMove_Action?.Invoke();
    }

    private void OnEndMove(Transform transform)
    {
        OnEndMove_Action?.Invoke(transform, currentPseudoChip.ChipData);
    }
    public event Action<PseudoChip> OnGrabPseudoChip_Action;

    public event Action<Vector2> OnMove_Action;

    public event Action OnStartMove_Action;

    public event Action<Transform, ChipData> OnEndMove_Action;

    #endregion
}
