using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDesignView : View
{
    [SerializeField] private Image imageBackground;

    public void SetGameDesign(GameDesign gameDesign)
    {
        imageBackground.sprite = gameDesign.Sprite;
    }
}
