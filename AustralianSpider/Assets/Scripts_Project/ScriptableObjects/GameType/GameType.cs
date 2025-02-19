using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameType", menuName = "Game/Type/GameType")]
public class GameType : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private List<CardType> cardTypes;

    private GameTypeData gameTypeData;

    public int ID => id;
    public List<CardType> CardTypes => cardTypes;

    public GameTypeData GameTypeData => gameTypeData;

    internal void SetData(GameTypeData gameTypeData)
    {
        this.gameTypeData = gameTypeData;
    }
}
