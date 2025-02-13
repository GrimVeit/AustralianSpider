using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardComboView : MonoBehaviour
{
    [SerializeField] private Image leftCardImage;
    [SerializeField] private Image rightCardImage;

    public void SetData(CardValue leftCardValue, CardValue rightCardValue)
    {
        leftCardImage.sprite = leftCardValue.CardSprite;
        rightCardImage.sprite = rightCardValue.CardSprite;
    }
}
