using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "CoverCardDesignGroup", menuName = "Cards/CardDesign/CoverCardDesignGroup")]
public class CoverCardDesignGroup : ScriptableObject
{
    public List<CoverCardDesign> CoverCardDesigns = new List<CoverCardDesign>();

    public CoverCardDesign GetCoverCardDesignById(int id)
    {
        return CoverCardDesigns.FirstOrDefault(data => data.ID == id);
    }
}
