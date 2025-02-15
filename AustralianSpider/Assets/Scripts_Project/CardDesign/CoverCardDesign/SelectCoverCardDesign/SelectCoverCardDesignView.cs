using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectCoverCardDesignView : View
{
    [SerializeField] private SelectCoverCardDesign selectCoverCardDesignPrefab;
    [SerializeField] private Transform transformParent;

    private List<SelectCoverCardDesign> selectCoverCardDesigns = new List<SelectCoverCardDesign>();

    public void Initialize()
    {

    }

    public void SetOpenCoverCardDesign(CoverCardDesign design)
    {
        var faceCardDesign = selectCoverCardDesigns.FirstOrDefault(data => data.ID == design.ID);

        if (faceCardDesign == null)
        {
            //Debug.Log("KKK");
            faceCardDesign = Instantiate(selectCoverCardDesignPrefab, transformParent);
            faceCardDesign.OnChooseCoverCardDesign += HandleChooseCoverCardDesign;
            faceCardDesign.SetData(design);
            faceCardDesign.Initialize();

            selectCoverCardDesigns.Add(faceCardDesign);
        }

        //Debug.Log("KKK");

        faceCardDesign.OpenDesign();
    }

    public void SetCloseCoverCardDesign(CoverCardDesign design)
    {
        var faceCardDesign = selectCoverCardDesigns.FirstOrDefault(data => data.ID == design.ID);

        if (faceCardDesign == null)
        {
            faceCardDesign = Instantiate(selectCoverCardDesignPrefab, transformParent);
            faceCardDesign.OnChooseCoverCardDesign += HandleChooseCoverCardDesign;
            faceCardDesign.SetData(design);
            faceCardDesign.Initialize();

            selectCoverCardDesigns.Add(faceCardDesign);
        }

        faceCardDesign.CloseDesign();
    }

    public void SelectCoverCardDesign(int id)
    {
        selectCoverCardDesigns.FirstOrDefault(data => data.ID == id).SelectDesign();
    }

    public void DeselectCoverCardDesign(int id)
    {
        selectCoverCardDesigns.FirstOrDefault(data => data.ID == id).DeselectDesign();
    }


    public void Dispose()
    {
        selectCoverCardDesigns.ForEach((data) =>
        {
            data.OnChooseCoverCardDesign -= HandleChooseCoverCardDesign;
            data.Dispose();
        });

        selectCoverCardDesigns.Clear();
    }

    #region Input

    public event Action<int> OnChooseCoverCardDesign;

    private void HandleChooseCoverCardDesign(int id)
    {
        OnChooseCoverCardDesign?.Invoke(id);
    }

    #endregion
}
