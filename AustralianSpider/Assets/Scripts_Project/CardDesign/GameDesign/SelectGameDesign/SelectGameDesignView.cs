using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectGameDesignView : View
{
    [SerializeField] private SelectGameDesign selectGameDesignPrefab;
    [SerializeField] private Transform transformParent;

    private List<SelectGameDesign> selectGameDesigns = new List<SelectGameDesign>();

    public void Initialize()
    {

    }

    public void SetOpenCoverCardDesign(GameDesign design)
    {
        var gameDesign = selectGameDesigns.FirstOrDefault(data => data.ID == design.ID);

        if (gameDesign == null)
        {
            //Debug.Log("KKK");
            gameDesign = Instantiate(selectGameDesignPrefab, transformParent);
            gameDesign.OnChooseGameDesign += HandleChooseGameDesign;
            gameDesign.SetData(design);
            gameDesign.Initialize();

            selectGameDesigns.Add(gameDesign);
        }

        //Debug.Log("KKK");

        gameDesign.OpenDesign();
    }

    public void SetCloseCoverCardDesign(GameDesign design)
    {
        var gameDesign = selectGameDesigns.FirstOrDefault(data => data.ID == design.ID);

        if (gameDesign == null)
        {
            gameDesign = Instantiate(selectGameDesignPrefab, transformParent);
            gameDesign.OnChooseGameDesign += HandleChooseGameDesign;
            gameDesign.SetData(design);
            gameDesign.Initialize();

            selectGameDesigns.Add(gameDesign);
        }

        gameDesign.CloseDesign();
    }

    public void SelectGameDesign(int id)
    {
        selectGameDesigns.FirstOrDefault(data => data.ID == id).SelectDesign();
    }

    public void DeselectGameDesign(int id)
    {
        selectGameDesigns.FirstOrDefault(data => data.ID == id).DeselectDesign();
    }


    public void Dispose()
    {
        selectGameDesigns.ForEach((data) =>
        {
            data.OnChooseGameDesign -= HandleChooseGameDesign;
            data.Dispose();
        });

        selectGameDesigns.Clear();
    }

    #region Input

    public event Action<int> OnChooseGameDesign;

    private void HandleChooseGameDesign(int id)
    {
        OnChooseGameDesign?.Invoke(id);
    }

    #endregion
}
