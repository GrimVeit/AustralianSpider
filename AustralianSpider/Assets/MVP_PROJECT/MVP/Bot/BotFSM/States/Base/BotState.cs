using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBotState
{
    public void Initialize();
    public void Dispose();
    public void EnterState();
    public void ExitState();
    public void UpdateState();
}
