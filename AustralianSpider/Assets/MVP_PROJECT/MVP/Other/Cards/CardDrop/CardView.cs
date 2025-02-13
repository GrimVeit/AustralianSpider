using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] private Image image;

    public void SetData(CardValue cardValue)
    {
        image.sprite = cardValue.CardSprite;
    }
}
