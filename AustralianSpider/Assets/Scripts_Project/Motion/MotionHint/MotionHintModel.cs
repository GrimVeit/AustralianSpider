using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionHintModel : MonoBehaviour
{
    public event Action OnMotionHint;

    public void MotionHint()
    {
        OnMotionHint?.Invoke();
    }
}
