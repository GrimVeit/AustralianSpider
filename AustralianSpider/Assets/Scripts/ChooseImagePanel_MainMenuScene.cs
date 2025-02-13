using System;
using UnityEngine;
using UnityEngine.UI;

public class ChooseImagePanel_MainMenuScene : MovePanel
{
    public event Action OnClickToBackInRegistrationButton;

    [SerializeField] private Button backToRegistrationButton;

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        backToRegistrationButton.onClick.AddListener(HandlerClickToBackinRegistrationButton);
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();

        backToRegistrationButton.onClick.RemoveListener(HandlerClickToBackinRegistrationButton);
    }

    private void HandlerClickToBackinRegistrationButton()
    {
        OnClickToBackInRegistrationButton?.Invoke();
    }
}
