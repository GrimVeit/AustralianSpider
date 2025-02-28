using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel_Menu : MovePanel
{
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonDesign;
    [SerializeField] private Button buttonDailyTask;

    public override void Initialize()
    {
        base.Initialize();

        buttonPlay.onClick.AddListener(()=> OnClickToPlay?.Invoke());
        buttonDesign.onClick.AddListener(()=> OnClickToDesign?.Invoke());
        buttonDailyTask.onClick.AddListener(()=> OnClickToDailyTask?.Invoke());
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonPlay.onClick.RemoveListener(() => OnClickToPlay?.Invoke());
        buttonDesign.onClick.RemoveListener(() => OnClickToDesign?.Invoke());
        buttonDailyTask.onClick.RemoveListener(() => OnClickToDailyTask?.Invoke());
    }

    #region Input

    public event Action OnClickToPlay;
    public event Action OnClickToDesign;
    public event Action OnClickToDailyTask;

    #endregion
}
