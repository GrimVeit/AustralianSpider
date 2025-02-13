using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class NicknameModel
{
    public event Action<string> OnGetNickname;

    public event Action OnCorrectNickname;
    public event Action OnIncorrectNickname;
    public event Action<string> OnEnterRegisterLoginError;

    public event Action<string> OnGetRandomNickname;

    private readonly Regex mainRegex = new("^[a-zA-Z0-9._]*$");
    private readonly Regex invalidRegex = new(@"(\.{2,}|/{2,})");
    private const string URL = "https://dinoipsum.com/api/?format=text&paragraphs=1&words=1";

    public string Nickname { get; private set; }

    private readonly string keyNickname;

    private ISoundProvider soundProvider;

    public NicknameModel(string keyNickname, ISoundProvider soundProvider)
    {
        this.keyNickname = keyNickname;
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {
        Nickname = PlayerPrefs.GetString(keyNickname, "Error");
        OnGetNickname?.Invoke(Nickname);
    }

    public void ChangeNickname(string value)
    {
        Nickname = value;
        OnGetNickname?.Invoke(Nickname);

        soundProvider.PlayOneShot("TextEnter");

        if (value.Length < 5)
        {
            OnEnterRegisterLoginError?.Invoke("Nickname must be at least 5 characters long");
            OnIncorrectNickname?.Invoke();
            return;
        }

        if (value.Length > 17)
        {
            OnEnterRegisterLoginError?.Invoke("Nickname must not exceed 17 characters");
            OnIncorrectNickname?.Invoke();
            return;
        }

        if (!mainRegex.IsMatch(value))
        {
            OnEnterRegisterLoginError?.Invoke("Nickname can only contain english letters, numbers, periods and slashes");
            OnIncorrectNickname?.Invoke();
            return;
        }

        if (invalidRegex.IsMatch(value))
        {
            OnEnterRegisterLoginError?.Invoke("Nickname cannot contain consencutive periods and slashes");
            OnIncorrectNickname?.Invoke();
            return;
        }

        if (value.EndsWith("."))
        {
            OnEnterRegisterLoginError?.Invoke("Nickname cannot end with a period");
            return;
        }

        OnEnterRegisterLoginError?.Invoke("");
        OnCorrectNickname?.Invoke();
    }

    public void RandomNickname()
    {
        Coroutines.Start(RandomizerCoroutine());
    }

    private IEnumerator RandomizerCoroutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(URL);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("No randomizing nickname");
            OnGetRandomNickname?.Invoke("NoConnect");
            yield break;
        }

        string nick = request.downloadHandler.text;

        OnGetRandomNickname?.Invoke(nick.Remove(nick.Length - 3));
    }
}
