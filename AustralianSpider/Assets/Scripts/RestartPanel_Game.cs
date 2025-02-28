using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartPanel_Game : MovePanel
{
    [SerializeField] private Button buttonRestart;
    [SerializeField] private Button buttonCancel;

    public override void Initialize()
    {
        base.Initialize();

        buttonRestart.onClick.AddListener(() => OnClickToRestart?.Invoke());
        buttonCancel.onClick.AddListener(() => OnClickToCancel?.Invoke());
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonRestart.onClick.RemoveListener(() => OnClickToRestart?.Invoke());
        buttonCancel.onClick.RemoveListener(() => OnClickToCancel?.Invoke());
    }

    #region Input

    public event Action OnClickToRestart;
    public event Action OnClickToCancel;

    #endregion
}
