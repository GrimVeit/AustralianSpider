using System;
using UnityEngine;

public class RouletteModel
{
    public event Action<RouletteSlotValue> OnGetRouletteSlotValue;
    public event Action<Vector3> OnRollBallToSlot;
    public event Action OnStartSpin;

    private ISoundProvider soundProvider;

    public RouletteModel(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void StartSpin()
    {
        soundProvider.PlayOneShot("RouletteWheel");
        OnStartSpin?.Invoke();
    }

    public void GetRouletteNumber(RouletteSlotValue rouletteSlotValue)
    {
        OnGetRouletteSlotValue?.Invoke(rouletteSlotValue);
    }

    public void RollBallToSlot(Vector3 vector)
    {
        OnRollBallToSlot?.Invoke(vector);
    }
}
