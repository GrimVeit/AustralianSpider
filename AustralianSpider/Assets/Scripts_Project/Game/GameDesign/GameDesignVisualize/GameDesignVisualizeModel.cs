using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDesignVisualizeModel
{
    public event Action<GameDesign> OnSetGameDesign;

    public void SetGameDesign(GameDesign design)
    {
        OnSetGameDesign?.Invoke(design);
    }
}
