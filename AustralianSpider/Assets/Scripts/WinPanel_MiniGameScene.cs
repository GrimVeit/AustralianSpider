using System;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel_MiniGameScene : MovePanel
{
    [SerializeField] private Button buttonExit;
    [SerializeField] private Button buttonRestart;

    private ISoundProvider soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public override void Initialize()
    {
        base.Initialize();

        buttonExit.onClick.AddListener(HandleClickToExitButton);
        buttonRestart.onClick.AddListener(HandleClickToRestartButton);
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonExit.onClick.RemoveListener(HandleClickToExitButton);
        buttonRestart.onClick.RemoveListener(HandleClickToRestartButton);
    }

    #region Input

    public event Action OnClickToButtonExit;
    public event Action OnClickToButtonRestart;

    private void HandleClickToExitButton()
    {
        soundProvider.PlayOneShot("Button_Click");
        OnClickToButtonExit?.Invoke();
    }

    private void HandleClickToRestartButton()
    {
        soundProvider.PlayOneShot("Button_Click");
        OnClickToButtonRestart?.Invoke();
    }

    #endregion
}
