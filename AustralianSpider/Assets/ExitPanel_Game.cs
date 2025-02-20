using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitPanel_Game : MovePanel
{
    [SerializeField] private Button buttonExit;
    [SerializeField] private Button buttonCancel;

    public override void Initialize()
    {
        base.Initialize();

        buttonExit.onClick.AddListener(() => OnClickToExit?.Invoke());
        buttonCancel.onClick.AddListener(() => OnClickToCancel?.Invoke());
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonExit.onClick.RemoveListener(() => OnClickToExit?.Invoke());
        buttonCancel.onClick.RemoveListener(() => OnClickToCancel?.Invoke());
    }

    #region Input

    public event Action OnClickToExit;
    public event Action OnClickToCancel;

    #endregion
}
