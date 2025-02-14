using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCoverCardDesignModel
{
    public event Action<int> OnChooseCoverCardDesign;

    public event Action<CoverCardDesign> OnSetOpenCoverCardDesign;
    public event Action<CoverCardDesign> OnSetCloseCoverCardDesign;

    public event Action<int> OnSelectCoverCardDesign;
    public event Action<int> OnDeselectCoverCardDesign;


    public void SetOpenCoverCardDesign(CoverCardDesign design)
    {
        OnSetOpenCoverCardDesign?.Invoke(design);
    }

    public void SetCloseFaceCardDesign(CoverCardDesign design)
    {
        OnSetCloseCoverCardDesign?.Invoke(design);
    }


    public void SelectCoverCardDesign(int index)
    {
        OnSelectCoverCardDesign?.Invoke(index);
    }

    public void DeselectFaceCardDesign(int index)
    {
        OnDeselectCoverCardDesign?.Invoke(index);
    }




    public void ChooseFaceCardDesign(int id)
    {
        OnChooseCoverCardDesign?.Invoke(id);
    }
}
