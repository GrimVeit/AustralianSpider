using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverCardDesignVisualizeModel
{
    public event Action<CoverCardDesign> OnSetCoverCardDesign;

    public void SetCoverCardDesign(CoverCardDesign design)
    {
        OnSetCoverCardDesign?.Invoke(design);
    }
}
