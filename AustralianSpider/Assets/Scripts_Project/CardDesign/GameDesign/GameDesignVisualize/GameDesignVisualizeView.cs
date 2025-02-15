using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDesignVisualizeView : View
{
    [SerializeField] private Image imageBackground;

    public void SetGameDesign(GameDesign design)
    {
        imageBackground.sprite = design.Sprite;
    }
}
