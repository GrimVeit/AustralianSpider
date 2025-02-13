using System;
using UnityEngine;
using UnityEngine.UI;

public class FinishPanel_GameSoloScene : MovePanel
{
    [SerializeField] private Button buttonBack;
    [SerializeField] private Button buttonRestart;

    public override void Initialize()
    {
        base.Initialize();

        buttonBack.onClick.AddListener(HandlerClickToBackButton);
        buttonRestart.onClick.AddListener(HandlerClickToRestartButton);
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonBack.onClick.RemoveListener(HandlerClickToBackButton);
        buttonRestart.onClick.RemoveListener(HandlerClickToRestartButton);
    }

    #region Input

    public event Action OnGoToMainMenu;
    public event Action OnGoToSoloGame;

    private void HandlerClickToBackButton()
    {
        OnGoToMainMenu?.Invoke();
    }

    private void HandlerClickToRestartButton()
    {
        OnGoToSoloGame?.Invoke();
    }

    #endregion
}
