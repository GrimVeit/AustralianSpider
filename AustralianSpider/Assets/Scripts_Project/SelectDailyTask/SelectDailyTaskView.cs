using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectDailyTaskView : View
{
    [SerializeField] private GameObject objectBlank;
    [SerializeField] private SelectDailyTask selectDailyTaskPrefab;
    [SerializeField] private Transform content;

    private List<SelectDailyTask> tasks = new List<SelectDailyTask>();

    public void Initialize()
    {

    }

    public void Dispose()
    {
        tasks.ForEach(t =>
        {
            t.OnSelectDailyTask -= HandleChooseDailyTask;
            t.Dispose();
        });
    }

    public void SetDayOfWeakFirstDayMonth(DayOfWeek dayOfWeek)
    {
        Debug.Log(dayOfWeek.ToString());

        switch (dayOfWeek)
        {
            case DayOfWeek.Sunday:
                break;
            case DayOfWeek.Monday:
                SpawnBlanks(1);
                break;
            case DayOfWeek.Tuesday:
                SpawnBlanks(2);
                break;
            case DayOfWeek.Wednesday:
                SpawnBlanks(3);
                break;
            case DayOfWeek.Thursday:
                SpawnBlanks(4);
                break;
            case DayOfWeek.Friday:
                SpawnBlanks(5);
                break;
            case DayOfWeek.Saturday:
                SpawnBlanks(6);
                break;
        }
    }

    public void SetDailyTaskData(DailyTaskData taskData)
    {
        var task = tasks.FirstOrDefault(data => data.Id == taskData.Id);

        if(task == null)
        {
            task = Instantiate(selectDailyTaskPrefab, content);
            task.OnSelectDailyTask += HandleChooseDailyTask;
            task.SetData(taskData);
            task.Initialize();

            tasks.Add(task);
        }

        ChooseAction(task, taskData.Status);
    }


    public void SelectDailyTask(int id)
    {
        tasks.FirstOrDefault(data => data.Id == id).Select();
    }

    public void DeselectDailyTask(int id)
    {
        tasks.FirstOrDefault(data => data.Id == id).Deselect();
    }


    private void ChooseAction(SelectDailyTask task, DailyTaskStatus status)
    {
        switch (status)
        {
            case DailyTaskStatus.NonePlayed:
                task.NonePlayed();
                break;
            case DailyTaskStatus.WinPlayed:
                task.WinPlayed();
                break;
            case DailyTaskStatus.LosePlayed:
                task.LosePlayed();
                break;
            case DailyTaskStatus.SkippedPlayed:
                task.SkipPlayed();
                break;
        }
    }

    private void SpawnBlanks(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(objectBlank, content);
        }
    }

    #region Input

    public event Action<int> OnChooseDailyTask;

    private void HandleChooseDailyTask(int id)
    {
        OnChooseDailyTask?.Invoke(id);
    }

    #endregion
}
