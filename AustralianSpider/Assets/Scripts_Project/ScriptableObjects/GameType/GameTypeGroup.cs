using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "GameTypeGroup", menuName = "Game/Type/GameTypeGroup")]
public class GameTypeGroup : ScriptableObject
{
    public List<GameType> GameTypes = new List<GameType>();

    public GameType GetGameDesignById(int id)
    {
        return GameTypes.FirstOrDefault(data => data.ID == id);
    }
}
