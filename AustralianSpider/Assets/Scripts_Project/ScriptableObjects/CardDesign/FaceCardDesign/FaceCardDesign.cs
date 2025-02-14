using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FaceCardDesign", menuName = "Cards/CardDesign/FaceCardDesign")]
public class FaceCardDesign : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private Sprite spriteDesign;
    [SerializeField] private Cards clubs_Krests;
    [SerializeField] private Cards diamonds_Bubns;
    [SerializeField] private Cards hearts_Chervs;
    [SerializeField] private Cards spades_Peaks;
    private FaceCardDesignData designData;

    public int ID => id;

    public Sprite SpriteDesign => spriteDesign;

    public Cards Clubs_Krests => clubs_Krests;
    public Cards Diamonds_Bubns => diamonds_Bubns;
    public Cards Hearts_Chervs => hearts_Chervs;
    public Cards Spades_Peaks => spades_Peaks;
    public FaceCardDesignData DesignData => designData;

    internal void SetData(FaceCardDesignData faceCardDesignData)
    {
        this.designData = faceCardDesignData;
    }
}

//    Clubs_Krest,
//    Diamonds_Bubna,
//    Heart_Cherv,
//    Spade_Peak
