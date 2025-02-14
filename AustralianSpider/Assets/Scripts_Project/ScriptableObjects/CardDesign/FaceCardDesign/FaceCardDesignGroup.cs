using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "FaceCardDesignGroup", menuName = "Cards/CardDesign/FaceCardDesignGroup")]
public class FaceCardDesignGroup : ScriptableObject
{
    public List<FaceCardDesign> FaceCardDesigns = new List<FaceCardDesign>();

    public FaceCardDesign GetFaceCardDesignById(int id)
    {
        return FaceCardDesigns.FirstOrDefault(data => data.ID == id);
    }
}
