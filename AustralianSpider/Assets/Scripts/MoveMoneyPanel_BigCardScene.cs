using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveMoneyPanel_BigCardScene : MovePanel
{
    [SerializeField] private Button buttonContinue;
    [SerializeField] private Button buttonExit;

    private ISoundProvider soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        buttonContinue.onClick.AddListener(HandlerClickToContinueButton);
        buttonExit.onClick.AddListener(HandlerClickToExitButton);
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();

        buttonContinue.onClick.RemoveListener(HandlerClickToContinueButton);
        buttonExit.onClick.RemoveListener(HandlerClickToExitButton);
    }

    public event Action OnClickToContinueButton;
    public event Action OnClickToExitButton;

    private void HandlerClickToContinueButton()
    {
        soundProvider.PlayOneShot("ClickClose");
        OnClickToContinueButton?.Invoke();
    }

    private void HandlerClickToExitButton()
    {
        soundProvider.PlayOneShot("ClickClose");
        OnClickToExitButton?.Invoke();
    }
}
