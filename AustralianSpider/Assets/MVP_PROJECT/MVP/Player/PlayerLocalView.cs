using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLocalView : View, IPlayerView, IIdentify
{
    public string GetID() => id;
    [SerializeField] private string id;

    [SerializeField] private List<TextMeshProUGUI> textNicknames;
    [SerializeField] private List<Image> imageAvatars;
    [SerializeField] private List<Sprite> spritesAvatars;

    public event Action<string> OnEnterNickname;
    public event Action<int> OnChooseImage;
    public event Action<string> OnSubmit;
    [SerializeField] private TMP_InputField inputFieldNickname;
    [SerializeField] private List<ImageElement> imageElements = new List<ImageElement>();
    [SerializeField] private Button buttonSubmitData;

    public void Initialize()
    {
        for (int i = 0; i < imageElements.Count; i++)
        {
            imageElements[i].OnChooseImage += HandlerClickToChooseImage;
            imageElements[i].Initialize(i);
        }

        buttonSubmitData.onClick.AddListener(HandlerClickToSubmitDataButton);
        inputFieldNickname.onValueChanged.AddListener(HandlerOnTextEnter);
    }

    public void Dispose()
    {
        for (int i = 0; i < imageElements.Count; i++)
        {
            imageElements[i].OnChooseImage -= HandlerClickToChooseImage;
            imageElements[i].Dispose();
        }

        buttonSubmitData.onClick.RemoveListener(HandlerClickToSubmitDataButton);
        inputFieldNickname.onValueChanged.AddListener(HandlerOnTextEnter);
    }

    public void ChooseAvatar(int index)
    {
        for (int i = 0; i < imageAvatars.Count; i++)
        {
            imageAvatars[i].sprite = spritesAvatars[index];
        }
    }

    public void ChooseNickname(string nickname)
    {
        for (int i = 0; i < textNicknames.Count; i++)
        {
            textNicknames[i].text = nickname;
        }
    }

    public void Select(int index)
    {
        imageElements[index].Select();
    }

    public void Deselect(int index)
    {
        imageElements[index].Deselect();
    }

    #region Input

    private void HandlerClickToChooseImage(int index)
    {
        OnChooseImage?.Invoke(index);
    }

    private void HandlerOnTextEnter(string nickname)
    {
        OnEnterNickname?.Invoke(nickname);
    }

    private void HandlerClickToSubmitDataButton()
    {
        OnSubmit?.Invoke(inputFieldNickname.text);
    }

    #endregion
}
