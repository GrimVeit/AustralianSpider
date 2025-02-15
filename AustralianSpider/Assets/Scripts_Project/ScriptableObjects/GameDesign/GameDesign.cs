using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameDesign", menuName = "Game/Design/GameDesign")]
public class GameDesign : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private Sprite spriteDesignDescription;
    [SerializeField] private Sprite sprite;
    private GameDesignData designData;

    public int ID => id;
    public Sprite SpriteDesignDescription => spriteDesignDescription;
    public Sprite Sprite => sprite;

    public GameDesignData DesignData => designData;

    internal void SetData(GameDesignData gameDesignData)
    {
        this.designData = gameDesignData;
    }
}
