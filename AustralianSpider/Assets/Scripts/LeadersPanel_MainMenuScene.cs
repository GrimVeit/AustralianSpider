using System;
using UnityEngine;
using UnityEngine.UI;

public class LeadersPanel_MainMenuScene : MovePanel
{
    public event Action OnClickBackButton;
    public event Action OnClickToChangeAvatarButton;

    [SerializeField] private Button backButton;
    [SerializeField] private Button buttonChangeAvatar;

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        backButton.onClick.AddListener(HandlerClickToBackButton);
        buttonChangeAvatar.onClick.AddListener(HandlerClickToChangeAvatarButton);
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();

        backButton.onClick.RemoveListener(HandlerClickToBackButton);
        buttonChangeAvatar.onClick.RemoveListener(HandlerClickToChangeAvatarButton);
    }

    private void HandlerClickToBackButton()
    {
        OnClickBackButton?.Invoke();
    }

    private void HandlerClickToChangeAvatarButton()
    {
        OnClickToChangeAvatarButton?.Invoke();
    }
}
