using System;
using UnityEngine;
using UnityEngine.UI;

public class RegistrationDonePanel_MainMenuScene : MovePanel
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

    public event Action OnClickToBackButton;

    private void HandlerClickToBackButton()
    {
        OnClickToBackButton?.Invoke();
    }

    #endregion
}
