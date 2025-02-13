using System;
using UnityEngine;

public class PseudoChipModel
{
    public event Action OnUngrabCurrentPseudoChip;
    public event Action<PseudoChip> OnGrabPseudoChip;
    public event Action<ChipData, ICell, Vector2> OnSpawnChip;

    public event Action OnStartMove;
    public event Action<Vector2> OnMove;
    public event Action OnEndMove;
    public event Action OnTeleporting;

    private bool isActive = true;

    private IMoneyProvider moneyProvider;
    private ISoundProvider soundProvider;

    public PseudoChipModel(IMoneyProvider moneyProvider, ISoundProvider soundProvider)
    {
        this.moneyProvider = moneyProvider;
        this.soundProvider = soundProvider;
    }

    public void GrabPseudoChip(PseudoChip pseudoChip)
    {
        OnUngrabCurrentPseudoChip?.Invoke();

        soundProvider.PlayOneShot("ChipGrab");

        if (moneyProvider.CanAfford(pseudoChip.ChipData.Nominal))
        {
            Debug.Log(moneyProvider.GetMoney() + "//" + pseudoChip.ChipData.Nominal);
            OnGrabPseudoChip?.Invoke(pseudoChip);
            return;
        }
    }

    public void StartMove()
    {
        if (!isActive) return;

        OnStartMove?.Invoke();
    }

    public void Move(Vector2 vector)
    {
        if (!isActive) return;

        OnMove?.Invoke(vector);
    }

    public void EndMove(Transform transform, ChipData chipData)
    {
        if (!isActive) return;

        Collider2D collider = Physics2D.OverlapPoint(transform.position);

        if(collider != null)
        {
            Debug.Log(collider.gameObject.name);

            if(collider.gameObject.TryGetComponent(out ICell cell))
            {
                OnSpawnChip?.Invoke(chipData, cell, transform.localPosition);
                Teleport();
                return;
            }
        }

        soundProvider.PlayOneShot("ChipWhoosh");
        OnEndMove?.Invoke();
    }

    public void Teleport()
    {
        OnTeleporting?.Invoke();
    }

    public void Activate()
    {
        isActive = true;
    }


    public void Deactivate()
    {
        isActive = false;
    }
}
