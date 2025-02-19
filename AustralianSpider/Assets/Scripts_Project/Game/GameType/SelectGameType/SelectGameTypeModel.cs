using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGameTypeModel
{
    public event Action<int> OnChooseGameType;

    public event Action<int> OnSelectGameType;
    public event Action<int> OnDeselectGameType;

    public void SelectGameType(int index)
    {
        OnSelectGameType?.Invoke(index);
    }

    public void DeselectGameType(int index)
    {
        OnDeselectGameType?.Invoke(index);
    }




    public void ChooseGameType(int id)
    {
        OnChooseGameType?.Invoke(id);
    }
}
