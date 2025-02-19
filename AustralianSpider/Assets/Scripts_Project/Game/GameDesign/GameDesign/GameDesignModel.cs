using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDesignModel
{
    public event Action<GameDesign> OnSetGameDesign;

    public void SetGameDesign(GameDesign gameDesign)
    {
        OnSetGameDesign?.Invoke(gameDesign);
    }
}
