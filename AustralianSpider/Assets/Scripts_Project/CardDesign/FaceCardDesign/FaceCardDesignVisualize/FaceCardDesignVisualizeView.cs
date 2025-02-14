using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceCardDesignVisualizeView : View
{
    [SerializeField] private Image imageKingBubna;
    [SerializeField] private Image imageQueenPeak;
    [SerializeField] private Image imageJackKrest;

    public void SetFaceCardDesign(FaceCardDesign design)
    {
        imageKingBubna.sprite = design.Diamonds_Bubns.GetCardById(12).Sprite;
        imageQueenPeak.sprite = design.Spades_Peaks.GetCardById(11).Sprite;
        imageJackKrest.sprite = design.Clubs_Krests.GetCardById(10).Sprite;
    }
}
