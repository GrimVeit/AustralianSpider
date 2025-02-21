using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel_Game : MovePanel
{
    [SerializeField] private Button buttonExit;
    [SerializeField] private Button buttonRestart;

    public override void Initialize()
    {
        base.Initialize();

        buttonExit.onClick.AddListener(() => OnClickToExit?.Invoke());
        buttonRestart.onClick.AddListener(() => OnClickToRestart?.Invoke());
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonExit.onClick.RemoveListener(() => OnClickToExit?.Invoke());
        buttonRestart.onClick.RemoveListener(() => OnClickToRestart?.Invoke());
    }

    #region Input

    public event Action OnClickToExit;
    public event Action OnClickToRestart;

    #endregion
}
