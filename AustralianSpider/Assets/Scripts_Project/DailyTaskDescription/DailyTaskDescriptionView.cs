using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyTaskDescriptionView : View
{
    [SerializeField] private TextMeshProUGUI textDescription;
    [SerializeField] private Button buttonPlay;

    [SerializeField] private TextMeshProUGUI textYear;
    [SerializeField] private TextMeshProUGUI textMonth;

    public void Initialize()
    {
        buttonPlay.onClick.AddListener(()=> OnClickToPlayDailyTaask?.Invoke());
    }

    public void Dispose()
    {
        buttonPlay.onClick.RemoveListener(() => OnClickToPlayDailyTaask?.Invoke());
    }

    public void ActivatePlay()
    {
        if(buttonPlay.isActiveAndEnabled) return;

        buttonPlay.gameObject.SetActive(true);
        buttonPlay.enabled = true;
    }

    public void DeactivatePlay()
    {
        if (!buttonPlay.isActiveAndEnabled) return;

        buttonPlay.gameObject.SetActive(false);
        buttonPlay.enabled = false;
    }

    public void SetDescription(string description)
    {
        textDescription.text = description;
    }

    public void SetYearMonth(int year, int month)
    {
        textYear.text = year.ToString();

        switch (month)
        {
            case 1:
                textMonth.text = "January";
                break;
            case 2:
                textMonth.text = "February";
                break;
            case 3:
                textMonth.text = "Mart";
                break;
            case 4:
                textMonth.text = "April";
                break;
            case 5:
                textMonth.text = "May";
                break;
            case 6:
                textMonth.text = "June";
                break;
            case 7:
                textMonth.text = "Jule";
                break;
            case 8:
                textMonth.text = "August";
                break;
            case 9:
                textMonth.text = "September";
                break;
            case 10:
                textMonth.text = "October";
                break;
            case 11:
                textMonth.text = "November";
                break;
            case 12:
                textMonth.text = "December";
                break;
        }
    }

    #region Input

    public event Action OnClickToPlayDailyTaask;

    #endregion
}
