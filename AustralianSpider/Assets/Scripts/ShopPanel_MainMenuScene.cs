using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel_MainMenuScene : MovePanel
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Button collections_Button;

    private ISoundProvider soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        backButton.onClick.AddListener(HandlerClickToBackButton);
        collections_Button.onClick.AddListener(HandleClickToCollectionButton);
        playButton.onClick.AddListener(HandleClickToPlayButton);
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();

        backButton.onClick.RemoveListener(HandlerClickToBackButton);
        collections_Button.onClick.RemoveListener(HandleClickToCollectionButton);
        playButton.onClick.RemoveListener(HandleClickToPlayButton);
    }

    #region Input

    public event Action OnClickBackButton;
    public event Action OnClickCollectionsButton;
    public event Action OnClickPlayButton;


    private void HandlerClickToBackButton()
    {
        soundProvider.PlayOneShot("Button_Click");
        OnClickBackButton?.Invoke();
    }

    private void HandleClickToCollectionButton()
    {
        soundProvider.PlayOneShot("Button_Click");
        OnClickCollectionsButton?.Invoke();
    }

    private void HandleClickToPlayButton()
    {
        soundProvider.PlayOneShot("Button_Click");
        OnClickPlayButton?.Invoke();
    }

    #endregion
}
