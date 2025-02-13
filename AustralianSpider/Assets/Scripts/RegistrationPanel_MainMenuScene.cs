using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegistrationPanel_MainMenuScene : MovePanel
{
    public event Action OnClickToChooseImageButton;

    [SerializeField] private Button chooseImageButton;

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        chooseImageButton.onClick.AddListener(HandlerClickToChooseImageButton);
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();

        chooseImageButton.onClick.RemoveListener(HandlerClickToChooseImageButton);
    }

    private void HandlerClickToChooseImageButton()
    {
        OnClickToChooseImageButton?.Invoke();
    }
}
