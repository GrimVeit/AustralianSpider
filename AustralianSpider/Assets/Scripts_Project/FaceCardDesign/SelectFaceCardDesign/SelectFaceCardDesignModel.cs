using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFaceCardDesignModel
{
    public event Action<int> OnChooseFaceCardDesign;

    public event Action<FaceCardDesign> OnSetOpenFaceCardDesign;
    public event Action<FaceCardDesign> OnSetCloseFaceCardDesign;

    public event Action<int> OnSelectFaceCardDesign;
    public event Action<int> OnDeselectFaceCardDesign;


    public void SetOpenFaceCardDesign(FaceCardDesign design)
    {
        OnSetOpenFaceCardDesign?.Invoke(design);
    }

    public void SetCloseFaceCardDesign(FaceCardDesign design)
    {
        OnSetCloseFaceCardDesign?.Invoke(design);
    }


    public void SelectFaceCardDesign(int index)
    {
        OnSelectFaceCardDesign?.Invoke(index);
    }

    public void DeselectFaceCardDesign(int index)
    {
        OnDeselectFaceCardDesign?.Invoke(index);
    }




    public void ChooseFaceCardDesign(int id)
    {
        OnChooseFaceCardDesign?.Invoke(id);
    }
}
