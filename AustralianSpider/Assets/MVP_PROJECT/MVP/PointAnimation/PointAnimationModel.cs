using System;
using UnityEngine;

public class PointAnimationModel
{
    public event Action OnPlayAnimation;
    public event Action<Vector3> OnPlayAnimation_Position;

    public void PlayAnimation()
    {
        OnPlayAnimation?.Invoke();
    }

    public void PlayAnimation(Vector3 vector)
    {
        OnPlayAnimation_Position?.Invoke(vector);
    }
}
