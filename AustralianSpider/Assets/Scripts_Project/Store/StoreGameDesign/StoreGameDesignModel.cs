using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class StoreGameDesignModel
{
    public event Action<GameDesign> OnOpenGameDesign;
    public event Action<GameDesign> OnCloseGameDesign;

    public event Action<GameDesign> OnDeselectGameDesign;
    public event Action<GameDesign> OnSelectGameDesign;


    private GameDesignGroup gameDesigns;

    private GameDesign currentGameDesign;
    private GameDesignData currentGameDesidnData;

    private List<GameDesignData> gameDesignDatas = new List<GameDesignData>();

    public readonly string FilePath = Path.Combine(Application.persistentDataPath, "GameDesign.json");

    public StoreGameDesignModel(GameDesignGroup gameDesigns)
    {
        this.gameDesigns = gameDesigns;
    }

    public void Initialize()
    {
        if (File.Exists(FilePath))
        {
            string loadedJson = File.ReadAllText(FilePath);
            GameDesignDatas gameDesignDatas = JsonUtility.FromJson<GameDesignDatas>(loadedJson);

            Debug.Log("Success");

            this.gameDesignDatas = gameDesignDatas.Datas.ToList();
        }
        else
        {
            Debug.Log("HDBNJJJJJJJJJJJJJJJJJJJJJJ");

            gameDesignDatas = new List<GameDesignData>();

            for (int i = 0; i < gameDesigns.GameDesigns.Count; i++)
            {
                if (i == 0)
                {
                    gameDesignDatas.Add(new GameDesignData(true, true));
                }
                else
                {
                    gameDesignDatas.Add(new GameDesignData(false, false));
                }
            }
        }

        for (int i = 0; i < gameDesigns.GameDesigns.Count; i++)
        {
            gameDesigns.GameDesigns[i].SetData(gameDesignDatas[i]);

            if (gameDesigns.GameDesigns[i].DesignData.IsOpen)
                OnOpenGameDesign?.Invoke(gameDesigns.GameDesigns[i]);
            else
                OnCloseGameDesign?.Invoke(gameDesigns.GameDesigns[i]);
        }

        SelectGameDesign(GetSelectCoverCardDesignIndex());
    }

    public void Dispose()
    {
        string json = JsonUtility.ToJson(new GameDesignDatas(gameDesignDatas.ToArray()));
        File.WriteAllText(FilePath, json);
    }

    public void BuyGameDesign(int number)
    {
        var gameDesign = gameDesigns.GetGameDesignById(number);

        if (gameDesign.DesignData.IsOpen) return;

        gameDesign.DesignData.IsOpen = true;
        OnOpenGameDesign?.Invoke(gameDesign);
    }

    public void SelectGameDesign(int number)
    {
        if (currentGameDesign != null)
        {
            currentGameDesign.DesignData.IsSelect = false;
            OnDeselectGameDesign?.Invoke(currentGameDesign);
        }

        currentGameDesign = gameDesigns.GetGameDesignById(number);

        if (currentGameDesign != null)
        {
            currentGameDesign.DesignData.IsSelect = true;
            OnSelectGameDesign?.Invoke(currentGameDesign);
        }
    }


    private int GetSelectCoverCardDesignIndex()
    {
        return gameDesigns.GameDesigns.FirstOrDefault(ship => ship.DesignData.IsSelect == true).ID;
    }
}

[Serializable]
public class GameDesignDatas
{
    public GameDesignData[] Datas;

    public GameDesignDatas(GameDesignData[] datas)
    {
        Datas = datas;
    }
}

[Serializable]
public class GameDesignData
{
    public bool IsOpen;
    public bool IsSelect;

    public GameDesignData(bool isOpen, bool isSelect)
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
