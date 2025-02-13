using System;
using UnityEngine;

public class PlayerModel : IPlayerModel
{
    public event Action<string> OnGetNickname;
    public event Action<int> OnGetAvatarIndex;
    public event Action<int> OnSelectIndex;
    public event Action<int> OnDeselectIndex;

    public string Nickname { get; private set; }
    public int AvatarIndex { get; private set; }

    private readonly string keyNickname;
    private readonly string keyAvatar;

    public PlayerModel(string keyNickname, string keyAvatar)
    {
        this.keyNickname = keyNickname;
        this.keyAvatar = keyAvatar;
    }

    public void Initialize()
    {
        Nickname = PlayerPrefs.GetString(keyNickname, "Error");
        AvatarIndex = PlayerPrefs.GetInt(keyAvatar, 0);

        OnGetAvatarIndex?.Invoke(AvatarIndex);
        OnGetNickname?.Invoke(Nickname);
    }

    public void Dispose()
    {

    }

    public void OnChangeNickname(string nickname)
    {

    }

    public void OnChangeAvatar(int avatarIndex)
    {

    }

    public void Submit(string i)
    {
         
    }
}
