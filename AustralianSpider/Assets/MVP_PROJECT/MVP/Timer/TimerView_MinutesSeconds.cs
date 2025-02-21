using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerView_MinutesSeconds : View, ITimerView, IIdentify
{
    public string GetID() => id;

    [SerializeField] private string id;
    [SerializeField] private List<TextMeshProUGUI> textCounts;

    public void ChangeTime(int sec)
    {
        int minutes = sec / 60;
        int seconds = sec % 60;

        textCounts.ForEach(text => text.text = $"{minutes}:{seconds:D2}");
    }

    public void ActivateTimer()
    {
        textCounts.ForEach(text => text.gameObject.SetActive(true));
    }

    public void DeactivateTimer()
    {
        textCounts.ForEach(text => text.gameObject.SetActive(false));
    }
}
