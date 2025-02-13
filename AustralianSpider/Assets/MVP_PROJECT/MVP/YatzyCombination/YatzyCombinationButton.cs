using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YatzyCombinationButton : MonoBehaviour
{
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Image imageButton;
    [SerializeField] private Sprite spriteActive;
    [SerializeField] private Sprite spriteInactive;

    public void Initialize()
    {
        buttonPlay.onClick.AddListener(HandlerClickToPlayButton);
    }

    public void Dispose()
    {
        buttonPlay.onClick.AddListener(HandlerClickToPlayButton);
    }

    public void ActivateButton()
    {
        buttonPlay.enabled = true;
        imageButton.sprite = spriteActive;
    }

    public void DeactivateButton()
    {
        buttonPlay.enabled = false;
        imageButton.sprite = spriteInactive;
    }

    #region Input

    public event Action OnClickToPlayButton;

    private void HandlerClickToPlayButton()
    {
        OnClickToPlayButton?.Invoke();
    }

    #endregion
}
