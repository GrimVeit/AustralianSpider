using System;
using System.Collections.Generic;
using UnityEngine;

public class ChipModel
{
    public event Action<List<Chip>> OnRecallAllChips;
    public event Action<Chip> OnNoneRetractChip;
    public event Action<Chip> OnRetractLastChip;
    public event Action<Chip> OnFallChip;

    public event Action<ChipData, ICell, Vector2> OnSpawn;

    public ISoundProvider soundProvider;

    public ChipModel(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void SpawnChip(ChipData chipData, ICell cell, Vector2 vector)
    {
        soundProvider.PlayOneShot("ChipDrop");
        OnSpawn?.Invoke(chipData, cell, vector);
    }

    public void RecallAllChips(List<Chip> chips)
    {
        if (chips.Count == 0) return;
        soundProvider.PlayOneShot("ChipWhoosh");
        OnRecallAllChips?.Invoke(chips);
    }

    public void RetractLastChip(Chip chip)
    {
        if (chip == null) return;
        soundProvider.PlayOneShot("ChipWhoosh");
        OnRetractLastChip?.Invoke(chip);
    }

    public void NoneRetractChip(Chip chip)
    {
        OnNoneRetractChip?.Invoke(chip);
    }

    public void FallChip(Chip chip)
    {
        OnFallChip?.Invoke(chip);
    }
}
