using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel_GameSoloScene : MovePanel
{
    [SerializeField] private Button buttonBack;

    public override void Initialize()
    {
        base.Initialize();

        buttonBack.onClick.AddListener(HandlerClickToBackButton);
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonBack.onClick.RemoveListener(HandlerClickToBackButton);
    }

    #region Input

    public event Action OnClickToGoMainMenu;

    private void HandlerClickToBackButton()
    {
        OnClickToGoMainMenu?.Invoke();
    }

    #endregion
}
