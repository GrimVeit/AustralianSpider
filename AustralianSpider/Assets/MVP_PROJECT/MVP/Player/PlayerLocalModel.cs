using System;
using System.Reflection;

public class PlayerLocalModel : IPlayerModel
{
    public event Action<int> OnSelectIndex;
    public event Action<int> OnDeselectIndex;

    public event Action<string> OnGetNickname;
    public event Action<int> OnGetAvatarIndex;

    public string Nickname { get; private set; } = null;
    public int AvatarIndex { get; private set; } = 0;

    private ISoundProvider soundProvider;
    public PlayerLocalModel(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {
        OnGetAvatarIndex?.Invoke(AvatarIndex);
        OnGetNickname?.Invoke(Nickname);
    }

    public void Dispose()
    {

    }

    public void Submit(string nick)
    {
        Nickname = nick;
        OnGetNickname?.Invoke(Nickname);
        soundProvider.PlayOneShot("Done");
    }

    public void OnChangeAvatar(int avatarIndex)
    {
        if (AvatarIndex == avatarIndex) return;

        soundProvider.PlayOneShot("SelectAvatar");

        if (AvatarIndex != avatarIndex)
        {
            OnDeselectIndex?.Invoke(AvatarIndex);
        }

        AvatarIndex = avatarIndex;
        OnSelectIndex?.Invoke(AvatarIndex);
        OnGetAvatarIndex?.Invoke(AvatarIndex);
    }

    public void OnChangeNickname(string nickname)
    {
        soundProvider.PlayOneShot("TextEnter");
        Nickname = nickname;
        OnGetNickname?.Invoke(Nickname);
    }
}
