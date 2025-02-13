using System;
using UnityEngine;

public class AvatarModel
{
    public event Action<int> OnSubmitChoose;

    public event Action<int> OnSelectIndex;
    public event Action<int> OnDeselectIndex;

    public int AvatarIndex { get; private set; } = 0;

    private readonly string keyAvatar;

    private ISoundProvider soundProvider;

    public AvatarModel(string keyAvatar, ISoundProvider soundProvider)
    {
        this.keyAvatar = keyAvatar;
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {
        AvatarIndex = PlayerPrefs.GetInt(keyAvatar, 0);
        OnSelectIndex?.Invoke(AvatarIndex);
        OnSubmitChoose?.Invoke(AvatarIndex);
    }

    public void Dispose()
    {
        PlayerPrefs.SetInt(keyAvatar, AvatarIndex);
    }

    public void ChooseAvatar(int avatarIndex)
    {
        if (AvatarIndex == avatarIndex) return;

        if (AvatarIndex != avatarIndex)
        {
            OnDeselectIndex?.Invoke(AvatarIndex);
        }

        soundProvider.PlayOneShot("SelectAvatar");
        AvatarIndex = avatarIndex;
        OnSelectIndex?.Invoke(AvatarIndex);
    }

    public void SubmitAvatar()
    {
        OnSubmitChoose?.Invoke(AvatarIndex);
    }
}
