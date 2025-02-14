using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoverCardDesignVisualizeView : View
{
    [SerializeField] private List<Image> images = new List<Image>();

    public void SetCoverCardDesign(CoverCardDesign design)
    {
        for (int i = 0; i < images.Count; i++)
        {
            images[i].sprite = design.SpriteDesign;
        }
    }
}
