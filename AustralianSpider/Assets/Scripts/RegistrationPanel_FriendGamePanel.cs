using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegistrationPanel_FriendGamePanel : MovePanel
{
    [SerializeField] private Button buttonChooseImageForFirstPlayer;
    [SerializeField] private Button buttonChooseImageForSecondPlayer;

    [SerializeField] private Button buttonPlay;

    public override void Initialize()
    {
        base.Initialize();

        buttonPlay.onClick.AddListener(HandlerClickToPlay);
        buttonChooseImageForFirstPlayer.onClick.AddListener(HandlerClickToChooseImageForFirstPlayerButton);
        buttonChooseImageForSecondPlayer.onClick.AddListener(HandlerClickToChooseImageForSecondPlayerButton);
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonPlay.onClick.RemoveListener(HandlerClickToPlay);
        buttonChooseImageForFirstPlayer.onClick.RemoveListener(HandlerClickToChooseImageForFirstPlayerButton);
        buttonChooseImageForSecondPlayer.onClick.RemoveListener(HandlerClickToChooseImageForSecondPlayerButton);
    }

    #region Input

    public event Action OnChooseImageForFirstPlayer;
    public event Action OnChooseImageForSecondPlayer;
    public event Action OnPlay;

    private void HandlerClickToChooseImageForFirstPlayerButton()
    {
        OnChooseImageForFirstPlayer?.Invoke();
    }

    private void HandlerClickToChooseImageForSecondPlayerButton()
    {
        OnChooseImageForSecondPlayer?.Invoke();
    }

    private void HandlerClickToPlay()
    {
        OnPlay?.Invoke();
    }

    #endregion
}
