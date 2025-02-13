using System;
using UnityEngine;
using UnityEngine.UI;

public class MoveWinningsPanel_BigCardScene : MovePanel
{
    [SerializeField] private Button buttonBack;

    private ISoundProvider soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        buttonBack.onClick.AddListener(HandlerClickToBackButton);
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();

        buttonBack.onClick.RemoveListener(HandlerClickToBackButton);
    }

    public event Action OnClickToBackButton;

    private void HandlerClickToBackButton()
    {
        soundProvider.PlayOneShot("ClickClose");
        OnClickToBackButton?.Invoke();
    }
}
