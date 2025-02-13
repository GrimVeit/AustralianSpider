using System;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel_RouletteScene : MovePanel
{
    [SerializeField] private Button buttonBack;
    [SerializeField] private Button buttonSpin;

    private ISoundProvider soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        buttonBack.onClick.AddListener(HandlerClickToBackButton);
        buttonSpin.onClick.AddListener(HandlerClickToSpinButton);
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();

        buttonBack.onClick.RemoveListener(HandlerClickToBackButton);
        buttonSpin.onClick.RemoveListener(HandlerClickToSpinButton);
    }

    #region Input

    public event Action OnClickToSpinButton;
    public event Action OnClickToBackButton;

    private void HandlerClickToBackButton()
    {
        soundProvider.PlayOneShot("Click");
        OnClickToBackButton?.Invoke();
    }

    private void HandlerClickToSpinButton()
    {
        soundProvider.PlayOneShot("Click");
        OnClickToSpinButton?.Invoke();
    }

    #endregion
}
