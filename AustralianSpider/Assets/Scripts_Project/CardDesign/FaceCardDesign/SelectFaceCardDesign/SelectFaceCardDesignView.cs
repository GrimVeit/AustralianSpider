using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectFaceCardDesignView : View
{
    [SerializeField] private SelectFaceCardDesign selectFaceCardDesignPrefab;
    [SerializeField] private Transform transformParent;

    private List<SelectFaceCardDesign> selectFaceCardDesigns = new List<SelectFaceCardDesign>();

    public void Initialize()
    {

    }

    public void SetOpenFaceCardDesign(FaceCardDesign design)
    {
        var faceCardDesign = selectFaceCardDesigns.FirstOrDefault(data => data.ID == design.ID);

        if(faceCardDesign == null)
        {
            Debug.Log("KKK");
            faceCardDesign = Instantiate(selectFaceCardDesignPrefab, transformParent);
            faceCardDesign.OnChooseFaceCardDesign += HandleChooseFaceCardDesign;
            faceCardDesign.SetData(design);
            faceCardDesign.Initialize();

            selectFaceCardDesigns.Add(faceCardDesign);
        }

        Debug.Log("KKK");

        faceCardDesign.OpenDesign();
    }

    public void SetCloseFaceCardDesign(FaceCardDesign design)
    {
        var faceCardDesign = selectFaceCardDesigns.FirstOrDefault(data => data.ID == design.ID);

        if (faceCardDesign == null)
        {
            faceCardDesign = Instantiate(selectFaceCardDesignPrefab, transformParent);
            faceCardDesign.OnChooseFaceCardDesign += HandleChooseFaceCardDesign;
            faceCardDesign.SetData(design);
            faceCardDesign.Initialize();

            selectFaceCardDesigns.Add(faceCardDesign);
        }

        faceCardDesign.CloseDesign();
    }

    public void SelectFaceCardDesign(int id)
    {
        selectFaceCardDesigns.FirstOrDefault(data => data.ID == id).SelectDesign();
    }

    public void DeselectFaceCardDesign(int id)
    {
        selectFaceCardDesigns.FirstOrDefault(data => data.ID == id).DeselectDesign();
    }


    public void Dispose()
    {
        selectFaceCardDesigns.ForEach((data) =>
        {
            data.OnChooseFaceCardDesign -= HandleChooseFaceCardDesign;
            data.Dispose();
        });

        selectFaceCardDesigns.Clear();
    }

    #region Input

    public event Action<int> OnChooseFaceCardDesign;

    private void HandleChooseFaceCardDesign(int id)
    {
        OnChooseFaceCardDesign?.Invoke(id);
    }

    #endregion
}
