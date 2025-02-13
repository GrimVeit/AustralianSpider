using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPresenter
{
    private IPlayerModel playerModel;
    private IPlayerView playerView;

    public PlayerPresenter(IPlayerModel playerModel, IPlayerView playerView)
    {
        this.playerModel = playerModel;
        this.playerView = playerView;
    }

    public void Initialize()
    {
        ActivateEvents();

        playerModel.Initialize();
        playerView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        playerModel.Dispose();
        playerView.Dispose();
    }

    private void ActivateEvents()
    {
        playerView.OnChooseImage += playerModel.OnChangeAvatar;
        playerView.OnEnterNickname += playerModel.OnChangeNickname;
        playerView.OnSubmit += playerModel.Submit;

        playerModel.OnSelectIndex += playerView.Select;
        playerModel.OnDeselectIndex += playerView.Deselect;
        playerModel.OnGetAvatarIndex += playerView.ChooseAvatar;
        playerModel.OnGetNickname += playerView.ChooseNickname;
    }

    private void DeactivateEvents()
    {
        playerView.OnChooseImage -= playerModel.OnChangeAvatar;
        playerView.OnEnterNickname -= playerModel.OnChangeNickname;
        playerView.OnSubmit -= playerModel.Submit;

        playerModel.OnSelectIndex -= playerView.Select;
        playerModel.OnDeselectIndex -= playerView.Deselect;
        playerModel.OnGetAvatarIndex -= playerView.ChooseAvatar;
        playerModel.OnGetNickname -= playerView.ChooseNickname;
    }
}

public interface IPlayerModel
{
    public event Action<int> OnSelectIndex;
    public event Action<int> OnDeselectIndex;
    public event Action<string> OnGetNickname;
    public event Action<int> OnGetAvatarIndex;

    public void OnChangeNickname(string nickname);

    public void OnChangeAvatar(int avatarIndex);
    public void Submit(string nickname);

    public void Initialize();
    public void Dispose();
}

public interface IPlayerView
{
    public event Action<string> OnSubmit;
    public event Action<int> OnChooseImage;
    public event Action<string> OnEnterNickname;

    public void ChooseAvatar(int index);
    public void ChooseNickname(string nickname);

    public void Initialize();
    public void Dispose();
    public void Select(int index);
    public void Deselect(int index);
}
