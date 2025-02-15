using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class StoreCoverCardDesignModel
{
    public event Action<CoverCardDesign> OnOpenCoverCardDesign;
    public event Action<CoverCardDesign> OnCloseCoverCardDesign;

    public event Action<CoverCardDesign> OnDeselectCoverCardDesign;
    public event Action<CoverCardDesign> OnSelectCoverCardDesign;


    private CoverCardDesignGroup coverCardDesigns;

    private CoverCardDesign currentCoverCardDesign;
    private CoverCardDesignData currentCoverCardDesidnData;

    private List<CoverCardDesignData> coverCardDesignDatas = new List<CoverCardDesignData>();

    public readonly string FilePath = Path.Combine(Application.persistentDataPath, "CoverCardDesign.json");

    public StoreCoverCardDesignModel(CoverCardDesignGroup faceCardDesigns)
    {
        this.coverCardDesigns = faceCardDesigns;
    }

    public void Initialize()
    {
        if (File.Exists(FilePath))
        {
            string loadedJson = File.ReadAllText(FilePath);
            CoverCardDesignDatas faceCardDesignDatas = JsonUtility.FromJson<CoverCardDesignDatas>(loadedJson);

            //Debug.Log("Success");

            this.coverCardDesignDatas = faceCardDesignDatas.Datas.ToList();
        }
        else
        {
            //Debug.Log("HDBNJJJJJJJJJJJJJJJJJJJJJJ");

            coverCardDesignDatas = new List<CoverCardDesignData>();

            for (int i = 0; i < coverCardDesigns.CoverCardDesigns.Count; i++)
            {
                if (i == 0)
                {
                    coverCardDesignDatas.Add(new CoverCardDesignData(true, true));
                }
                else
                {
                    coverCardDesignDatas.Add(new CoverCardDesignData(false, false));
                }
            }
        }

        for (int i = 0; i < coverCardDesigns.CoverCardDesigns.Count; i++)
        {
            coverCardDesigns.CoverCardDesigns[i].SetData(coverCardDesignDatas[i]);

            if (coverCardDesigns.CoverCardDesigns[i].DesignData.IsOpen)
                OnOpenCoverCardDesign?.Invoke(coverCardDesigns.CoverCardDesigns[i]);
            else
                OnCloseCoverCardDesign?.Invoke(coverCardDesigns.CoverCardDesigns[i]);
        }

        SelectCoverCardDesign(GetSelectCoverCardDesignIndex());
    }

    public void Dispose()
    {
        string json = JsonUtility.ToJson(new CoverCardDesignDatas(coverCardDesignDatas.ToArray()));
        File.WriteAllText(FilePath, json);
    }

    public void BuyCoverCardDesign(int number)
    {
        var faceCardDesign = coverCardDesigns.GetCoverCardDesignById(number);

        if (faceCardDesign.DesignData.IsOpen) return;

        faceCardDesign.DesignData.IsOpen = true;
        OnOpenCoverCardDesign?.Invoke(faceCardDesign);
    }

    public void SelectCoverCardDesign(int number)
    {
        if (currentCoverCardDesign != null)
        {
            currentCoverCardDesign.DesignData.IsSelect = false;
            OnDeselectCoverCardDesign?.Invoke(currentCoverCardDesign);
        }

        currentCoverCardDesign = coverCardDesigns.GetCoverCardDesignById(number);

        if (currentCoverCardDesign != null)
        {
            currentCoverCardDesign.DesignData.IsSelect = true;
            OnSelectCoverCardDesign?.Invoke(currentCoverCardDesign);
        }
    }


    private int GetSelectCoverCardDesignIndex()
    {
        return coverCardDesigns.CoverCardDesigns.FirstOrDefault(ship => ship.DesignData.IsSelect == true).ID;
    }
}

[Serializable]
public class CoverCardDesignDatas
{
    public CoverCardDesignData[] Datas;

    public CoverCardDesignDatas(CoverCardDesignData[] datas)
    {
        Datas = datas;
    }
}

[Serializable]
public class CoverCardDesignData
{
    public bool IsOpen;
    public bool IsSelect;

    public CoverCardDesignData(bool isOpen, bool isSelect)
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
