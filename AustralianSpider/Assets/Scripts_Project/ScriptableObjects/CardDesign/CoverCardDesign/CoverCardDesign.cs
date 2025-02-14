using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CoverCardDesign", menuName = "Cards/CardDesign/CoverCardDesign")]
public class CoverCardDesign : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private Sprite spriteDesign;
    private CoverCardDesignData designData;

    public int ID => id;
    public Sprite SpriteDesign => spriteDesign;

    public CoverCardDesignData DesignData => designData;

    internal void SetData(CoverCardDesignData coverCardDesignData)
    {
        this.designData = coverCardDesignData;
    }
}
