using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectGameTypeView : View
{
    [SerializeField] private List<SelectGameType> selectGameTypes = new List<SelectGameType>();

    public void Initialize()
    {
        selectGameTypes.ForEach((data) =>
        {
            data.OnChooseGameType += HandleChooseGameDesign;
            data.Initialize();
        });
    }


    public void Dispose()
    {
        selectGameTypes.ForEach((data) =>
        {
            data.OnChooseGameType -= HandleChooseGameDesign;
            data.Dispose();
        });

        selectGameTypes.Clear();
    }

    public void SelectGameType(int id)
    {
        selectGameTypes.FirstOrDefault(data => data.ID == id).SelectDesign();
    }

    public void DeselectGameType(int id)
    {
        selectGameTypes.FirstOrDefault(data => data.ID == id).DeselectDesign();
    }

    #region Input

    public event Action<int> OnChooseGameType;

    private void HandleChooseGameDesign(int id)
    {
        OnChooseGameType?.Invoke(id);
    }

    #endregion
}
