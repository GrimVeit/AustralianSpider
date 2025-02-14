using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCardDesignVisualizeModel : MonoBehaviour
{
    public event Action<FaceCardDesign> OnSetFaceCardDesign;

    public void SetFaceCardDesign(FaceCardDesign design)
    {
        OnSetFaceCardDesign?.Invoke(design);
    }
}
