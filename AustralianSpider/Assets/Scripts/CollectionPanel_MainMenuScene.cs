using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionPanel_MainMenuScene : MovePanel
{
    [SerializeField] private Button buttonBack;

    private ISoundProvider soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public override void Initialize()
    {
        base.Initialize();

        buttonBack.onClick.AddListener(HandleClickToBackButton);
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonBack.onClick.RemoveListener(HandleClickToBackButton);
    }

    public override void ActivatePanel()
    {
        base.ActivatePanel();
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();
    }

    #region Input

    public event Action OnClickToBackButton;

    private void HandleClickToBackButton()
    {
        soundProvider.PlayOneShot("Button_Click");
        OnClickToBackButton?.Invoke();
    }
    #endregion
}
