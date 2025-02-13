using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthStar : MonoBehaviour
{
    [SerializeField] private Image imageStar;
    
    public void SetSprite(Sprite sprite)
    {
        imageStar.sprite = sprite;
    }
}
