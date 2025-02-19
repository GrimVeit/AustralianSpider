using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class StoreGameTypeModel
{
    public event Action<GameType> OnDeselectGameType;
    public event Action<GameType> OnSelectGameType;


    private GameTypeGroup gameTypes;

    private GameType currentGameType;
    private GameTypeData currentGameTypeData;

    private List<GameTypeData> gameTypeDatas = new List<GameTypeData>();

    public readonly string FilePath = Path.Combine(Application.persistentDataPath, "GameType.json");

    public StoreGameTypeModel(GameTypeGroup gameTypes)
    {
        this.gameTypes = gameTypes;
    }

    public void Initialize()
    {
        if (File.Exists(FilePath))
        {
            string loadedJson = File.ReadAllText(FilePath);
            GameTypeDatas gameTypeDatas = JsonUtility.FromJson<GameTypeDatas>(loadedJson);

            Debug.Log("Success");

            this.gameTypeDatas = gameTypeDatas.Datas.ToList();
        }
        else
        {
            Debug.Log("HDBNJJJJJJJJJJJJJJJJJJJJJJ");

            gameTypeDatas = new List<GameTypeData>();

            for (int i = 0; i < gameTypes.GameTypes.Count; i++)
            {
                if (i == 0)
                {
                    gameTypeDatas.Add(new GameTypeData(true));
                }
                else
                {
                    gameTypeDatas.Add(new GameTypeData(false));
                }
            }
        }

        for (int i = 0; i < gameTypes.GameTypes.Count; i++)
        {
            gameTypes.GameTypes[i].SetData(gameTypeDatas[i]);
        }

        SelectGameType(GetSelectGameTypenIndex());
    }

    public void Dispose()
    {
        string json = JsonUtility.ToJson(new GameTypeDatas(gameTypeDatas.ToArray()));
        File.WriteAllText(FilePath, json);
    }

    public void SelectGameType(int number)
    {
        if (currentGameType != null)
        {
            currentGameType.GameTypeData.IsSelect = false;
            OnDeselectGameType?.Invoke(currentGameType);
        }

        currentGameType = gameTypes.GetGameDesignById(number);

        if (currentGameType != null)
        {
            currentGameType.GameTypeData.IsSelect = true;
            OnSelectGameType?.Invoke(currentGameType);
        }
    }


    private int GetSelectGameTypenIndex()
    {
        return gameTypes.GameTypes.FirstOrDefault(ship => ship.GameTypeData.IsSelect == true).ID;
    }
}

[Serializable]
public class GameTypeDatas
{
    public GameTypeData[] Datas;

    public GameTypeDatas(GameTypeData[] datas)
    {
        Datas = datas;
    }
}

[Serializable]
public class GameTypeData
{
    public bool IsSelect;

    public GameTypeData(bool isSelect)
    {
        this.IsSelect = isSelect;
    }

    public void Select()
    {
        IsSelect = true;
    }
}
