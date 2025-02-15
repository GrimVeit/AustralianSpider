using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StoreFaceCardDesignModel
{
    public event Action<FaceCardDesign> OnOpenFaceCardDesign;
    public event Action<FaceCardDesign> OnCloseFaceCardDesign;

    public event Action<FaceCardDesign> OnDeselectFaceCardDesign;
    public event Action<FaceCardDesign> OnSelectFaceCardDesign;


    private FaceCardDesignGroup faceCardDesigns;

    private FaceCardDesign currentFaceCardDesign;
    private FaceCardDesignData currentFaceCardDesidnData;

    private List<FaceCardDesignData> faceCardDesignDatas = new List<FaceCardDesignData>();

    public readonly string FilePath = Path.Combine(Application.persistentDataPath, "FaceCardDesign.json");

    public StoreFaceCardDesignModel(FaceCardDesignGroup faceCardDesigns)
    {
        this.faceCardDesigns = faceCardDesigns;
    }

    public void Initialize()
    {
        if (File.Exists(FilePath))
        {
            string loadedJson = File.ReadAllText(FilePath);
            FaceCardDesignDatas faceCardDesignDatas = JsonUtility.FromJson<FaceCardDesignDatas>(loadedJson);

            Debug.Log("Success");

            this.faceCardDesignDatas = faceCardDesignDatas.Datas.ToList();
        }
        else
        {
            Debug.Log("HDBNJJJJJJJJJJJJJJJJJJJJJJ");

            faceCardDesignDatas = new List<FaceCardDesignData>();

            for (int i = 0; i < faceCardDesigns.FaceCardDesigns.Count; i++)
            {
                if (i == 0)
                {
                    faceCardDesignDatas.Add(new FaceCardDesignData(true, true));
                }
                else
                {
                    faceCardDesignDatas.Add(new FaceCardDesignData(false, false));
                }
            }
        }

        for (int i = 0; i < faceCardDesigns.FaceCardDesigns.Count; i++)
        {
            faceCardDesigns.FaceCardDesigns[i].SetData(faceCardDesignDatas[i]);

            if (faceCardDesigns.FaceCardDesigns[i].DesignData.IsOpen)
                OnOpenFaceCardDesign?.Invoke(faceCardDesigns.FaceCardDesigns[i]);
            else
                OnCloseFaceCardDesign?.Invoke(faceCardDesigns.FaceCardDesigns[i]);
        }

        SelectFaceCardDesign(GetSelectShipIndex());
    }

    public void Dispose()
    {
        string json = JsonUtility.ToJson(new FaceCardDesignDatas(faceCardDesignDatas.ToArray()));
        File.WriteAllText(FilePath, json);
    }

    public void BuyFaceCardDesign(int number)
    {
        var faceCardDesign = faceCardDesigns.GetFaceCardDesignById(number);

        if (faceCardDesign.DesignData.IsOpen) return;

        faceCardDesign.DesignData.IsOpen = true;
        OnOpenFaceCardDesign?.Invoke(faceCardDesign);
    }

    public void SelectFaceCardDesign(int number)
    {
        if (currentFaceCardDesign != null)
        {
            currentFaceCardDesign.DesignData.IsSelect = false;
            OnDeselectFaceCardDesign?.Invoke(currentFaceCardDesign);
        }

        currentFaceCardDesign = faceCardDesigns.GetFaceCardDesignById(number);

        if (currentFaceCardDesign != null)
        {
            currentFaceCardDesign.DesignData.IsSelect = true;
            OnSelectFaceCardDesign?.Invoke(currentFaceCardDesign);
        }
    }


    private int GetSelectShipIndex()
    {
        return faceCardDesigns.FaceCardDesigns.FirstOrDefault(ship => ship.DesignData.IsSelect == true).ID;
    }
}

[Serializable]
public class FaceCardDesignDatas
{
    public FaceCardDesignData[] Datas;

    public FaceCardDesignDatas(FaceCardDesignData[] datas)
    {
        Datas = datas;
    }
}

[Serializable]
public class FaceCardDesignData
{
    public bool IsOpen;
    public bool IsSelect;

    public FaceCardDesignData(bool isOpen, bool isSelect)
    {
        this.IsOpen = isOpen;
        this.IsSelect = isSelect;
    }

    public void Select()
    {
        IsSelect = true;
    }

    public void Open()
    {
        IsOpen = true;
    }
}
