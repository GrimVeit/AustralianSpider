using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGameDesignModel
{
    public event Action<int> OnChooseGameDesign;

    public event Action<GameDesign> OnSetOpenGameDesign;
    public event Action<GameDesign> OnSetCloseGameDesign;

    public event Action<int> OnSelectGameDesign;
    public event Action<int> OnDeselectGameDesign;


    public void SetOpenGameDesign(GameDesign design)
    {
        OnSetOpenGameDesign?.Invoke(design);
    }

    public void SetCloseGameDesign(GameDesign design)
    {
        OnSetCloseGameDesign?.Invoke(design);
    }


    public void SelectGameDesign(int index)
    {
        OnSelectGameDesign?.Invoke(index);
    }

    public void DeselectGameDesign(int index)
    {
        OnDeselectGameDesign?.Invoke(index);
    }




    public void ChooseGameDesign(int id)
    {
        OnChooseGameDesign?.Invoke(id);
    }
}
