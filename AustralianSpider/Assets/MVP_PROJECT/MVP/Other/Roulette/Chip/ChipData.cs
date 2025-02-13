using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChipData
{
    [SerializeField] private int nominal;
    [SerializeField] private Sprite sprite;
    [SerializeField] private Transform parent;

    public int Nominal => nominal;
    public Sprite Sprite => sprite;
    public Transform Parent => parent;
}
