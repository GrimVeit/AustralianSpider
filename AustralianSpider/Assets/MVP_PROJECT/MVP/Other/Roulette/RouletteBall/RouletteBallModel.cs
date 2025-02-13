using System;
using UnityEngine;

public class RouletteBallModel
{
    public event Action<Vector3> OnBallStopped;
    public event Action OnStartSpin;

    private ISoundProvider soundProvider;

    public RouletteBallModel(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }
    public void StartSpin()
    {
        soundProvider.PlayOneShot("RouletteBallWheel");
        OnStartSpin?.Invoke();
    }

    public void BallStopped(Vector3 vector)
    {
        soundProvider.PlayOneShot("RouletteBallFallen");
        OnBallStopped?.Invoke(vector);
    }
}
