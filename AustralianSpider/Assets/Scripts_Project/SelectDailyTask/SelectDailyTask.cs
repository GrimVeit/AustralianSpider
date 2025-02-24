using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectDailyTask : MonoBehaviour, IPointerClickHandler
{
    public int Id => currentDailyTaskData.Id;

    [SerializeField] private GameObject objectSelect;

    [SerializeField] private TextMeshProUGUI textDay;
    [SerializeField] private GameObject objectWinPlayed;
    [SerializeField] private GameObject objectLosePlayed;

    private DailyTaskData currentDailyTaskData;

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    public void SetData(DailyTaskData dailyTaskData)
    {
        currentDailyTaskData = dailyTaskData;
        textDay.text = (currentDailyTaskData.Id + 1).ToString();
    }




    public void Select()
    {
        objectSelect.SetActive(true);
    }

    public void Deselect()
    {
        objectSelect.SetActive(false);
    }



    public void NonePlayed()
    {
        textDay.color = Color.black;

        objectWinPlayed.SetActive(false);
        objectLosePlayed.SetActive(false);
    }

    public void WinPlayed()
    {
        textDay.color = Color.gray;


        objectWinPlayed.SetActive(true);
        objectLosePlayed.SetActive(false);
    }

    public void LosePlayed()
    {
        textDay.color = Color.gray;


        objectWinPlayed.SetActive(false);
        objectLosePlayed.SetActive(true);
    }

    public void SkipPlayed()
    {
        textDay.color = Color.gray;


        objectWinPlayed.SetActive(false);
        objectLosePlayed.SetActive(true);
    }

    #region Input

    public event Action<int> OnSelectDailyTask;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnSelectDailyTask?.Invoke(currentDailyTaskData.Id);
    }

    #endregion
}
