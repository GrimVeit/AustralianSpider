using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PseudoChipPresenter
{
    private PseudoChipModel pseudoChipModel;
    private PseudoChipView pseudoChipView;

    public PseudoChipPresenter(PseudoChipModel pseudoChipModel, PseudoChipView pseudoChipView)
    {
        this.pseudoChipModel = pseudoChipModel;
        this.pseudoChipView = pseudoChipView;
    }

    public void Initialize()
    {
        ActivateEvents();

        pseudoChipView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        pseudoChipView.Dispose();
    }

    private void ActivateEvents()
    {
        pseudoChipView.OnGrabPseudoChip_Action += pseudoChipModel.GrabPseudoChip;
        pseudoChipView.OnStartMove_Action += pseudoChipModel.StartMove;
        pseudoChipView.OnMove_Action += pseudoChipModel.Move;
        pseudoChipView.OnEndMove_Action += pseudoChipModel.EndMove;

        pseudoChipModel.OnGrabPseudoChip += pseudoChipView.GrabPseudoChip;
        pseudoChipModel.OnUngrabCurrentPseudoChip += pseudoChipView.UngrabCurrentPseudoChip;
        pseudoChipModel.OnStartMove += pseudoChipView.StartMove;
        pseudoChipModel.OnMove += pseudoChipView.Move;
        pseudoChipModel.OnEndMove += pseudoChipView.EndMove;
        pseudoChipModel.OnTeleporting += pseudoChipView.Teleport;
    }

    private void DeactivateEvents()
    {
        pseudoChipView.OnGrabPseudoChip_Action -= pseudoChipModel.GrabPseudoChip;
        pseudoChipView.OnStartMove_Action -= pseudoChipModel.StartMove;
        pseudoChipView.OnMove_Action -= pseudoChipModel.Move;
        pseudoChipView.OnEndMove_Action -= pseudoChipModel.EndMove;

        pseudoChipModel.OnGrabPseudoChip -= pseudoChipView.GrabPseudoChip;
        pseudoChipModel.OnUngrabCurrentPseudoChip -= pseudoChipView.UngrabCurrentPseudoChip;
        pseudoChipModel.OnStartMove -= pseudoChipView.StartMove;
        pseudoChipModel.OnMove -= pseudoChipView.Move;
        pseudoChipModel.OnEndMove -= pseudoChipView.EndMove;
        pseudoChipModel.OnTeleporting -= pseudoChipView.Teleport;
    }

    #region Input

    public event Action<ChipData, ICell, Vector2> OnSpawnChip
    {
        add { pseudoChipModel.OnSpawnChip += value; }
        remove { pseudoChipModel.OnSpawnChip -= value; }
    }

    #endregion
}
