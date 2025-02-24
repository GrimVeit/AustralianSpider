using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class StoreDailyTaskModel
{
    public event Action<DayOfWeek> OnGetDayOfWeekFirstDayMonth;

    public event Action<DailyTaskData> OnDeselectDailyTask;
    public event Action<DailyTaskData> OnSelectDailyTask;

    public event Action<DailyTaskData> OnChangeStatusDailyTask;

    private DailyTaskData currentDailyTaskData;

    private List<DailyTaskData> dailyTaskDatas = new List<DailyTaskData>();

    public readonly string FilePath = Path.Combine(Application.persistentDataPath, "DailyTask.json");

    private int currentYear => DateTime.UtcNow.Year;
    private int currentMonth => DateTime.UtcNow.Month;
    private int currentDay => DateTime.UtcNow.Day;

    public void Initialize()
    {
        DayOfWeek dayOfweakFirstDayMonth = new DateTime(currentYear, currentMonth, 1).DayOfWeek;
        OnGetDayOfWeekFirstDayMonth?.Invoke(dayOfweakFirstDayMonth);

        if (File.Exists(FilePath))
        {
            string loadedJson = File.ReadAllText(FilePath);
            DailyTaskDatas dailyTaskDatas = JsonUtility.FromJson<DailyTaskDatas>(loadedJson);

            if(dailyTaskDatas.Month != currentMonth || dailyTaskDatas.Year != currentYear)
            {
                Debug.Log("CHANGE DATA");

                ResetCalendar();
            }
            else
            {
                Debug.Log("GOOD DATA");

                this.dailyTaskDatas = dailyTaskDatas.Datas.ToList();
            }
        }
        else
        {
            Debug.Log("NO DATA");

            ResetCalendar();
        }

        dailyTaskDatas.ForEach(task => OnChangeStatusDailyTask(task));

        SelectDailyTaskData(currentDay-1);
    }

    private void ResetCalendar()
    {
        Debug.Log("Reset calendar");

        dailyTaskDatas = new List<DailyTaskData>();

        for (int i = 0; i < DateTime.DaysInMonth(currentYear, currentMonth); i++)
        {
            if (i < currentDay - 1)
            {
                dailyTaskDatas.Add(new DailyTaskData(DailyTaskStatus.SkippedPlayed, false, i));
            }
            else if (i == currentDay - 1)
            {
                dailyTaskDatas.Add(new DailyTaskData(DailyTaskStatus.NonePlayed, true, i));
            }
            else
            {
                dailyTaskDatas.Add(new DailyTaskData(DailyTaskStatus.NonePlayed, false, i));
            }
        }
    }

    public void Dispose()
    {
        string json = JsonUtility.ToJson(new DailyTaskDatas(dailyTaskDatas.ToArray(), currentYear, currentMonth));
        File.WriteAllText(FilePath, json);
    }

    public void SelectDailyTaskData(int number)
    {
        if (currentDailyTaskData != null)
        {
            currentDailyTaskData.IsSelect = false;
            OnDeselectDailyTask?.Invoke(currentDailyTaskData);
        }

        currentDailyTaskData = GetDailyTaskById(number);

        if (currentDailyTaskData != null)
        {
            currentDailyTaskData.IsSelect = true;
            OnSelectDailyTask?.Invoke(currentDailyTaskData);
        }
    }

    public void SetWinStatus()
    {
        currentDailyTaskData.SetStatus(DailyTaskStatus.WinPlayed);
        OnChangeStatusDailyTask?.Invoke(currentDailyTaskData);
    }

    public void SetLoseStatus()
    {
        currentDailyTaskData.SetStatus(DailyTaskStatus.LosePlayed);
        OnChangeStatusDailyTask?.Invoke(currentDailyTaskData);
    }

    public DailyTaskData GetDailyTaskById(int id)
    {
        return dailyTaskDatas.FirstOrDefault(data => data.Id == id);
    }
}

[Serializable]
public class DailyTaskDatas
{
    public int Year;
    public int Month;
    public DailyTaskData[] Datas;

    public DailyTaskDatas(DailyTaskData[] datas, int year, int month)
    {
        Datas = datas;
        Year = year;
        Month = month;
    }
}

[Serializable]
public class DailyTaskData
{
    public int Id;
    public DailyTaskStatus Status;
    public bool IsSelect;

    public DailyTaskData(DailyTaskStatus status, bool isSelect, int id)
    {
        Status = status;
        IsSelect = isSelect;
        Id = id;
    }

    public void SetStatus(DailyTaskStatus status)
    {
        Status = status;
    }
}

public enum DailyTaskStatus
{
    NonePlayed,
    WinPlayed,
    LosePlayed,
    SkippedPlayed
}
