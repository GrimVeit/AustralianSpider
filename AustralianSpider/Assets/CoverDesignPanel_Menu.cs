using System;
using UnityEngine;
using UnityEngine.UI;

public class CoverDesignPanel_Menu : MovePanel
{
    [SerializeField] private Button buttonBack;

    public override void Initialize()
    {
        base.Initialize();

        buttonBack.onClick.AddListener(() => OnClickToBack?.Invoke());
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonBack.onClick.RemoveListener(() => OnClickToBack?.Invoke());
    }

    #region Input

    public event Action OnClickToBack;

    #endregion
}
