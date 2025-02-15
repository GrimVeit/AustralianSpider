using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "GameDesignGroup", menuName = "Game/Design/GameDesignGroup")]
public class GameDesignGroup : ScriptableObject
{
    public List<GameDesign> GameDesigns = new List<GameDesign>();

    public GameDesign GetGameDesignById(int id)
    {
        return GameDesigns.FirstOrDefault(data => data.ID == id);
    }
}
