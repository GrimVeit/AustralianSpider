using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteNumberHistoryView : MonoBehaviour
{
    [SerializeField] private Image imageRouletteNumber;

    public void SetData(RouletteNumber rouletteNumber)
    {
        imageRouletteNumber.sprite = rouletteNumber.Sprite;
    }
}
