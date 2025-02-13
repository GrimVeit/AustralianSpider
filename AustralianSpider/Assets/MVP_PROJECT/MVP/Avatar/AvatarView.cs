using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AvatarView : View, IIdentify
{
    public string GetID() => id;
    public event Action OnSubmitAvatar;
    public event Action<int> OnChooseAvatar;

    [SerializeField] private string id;
    [SerializeField] private List<Image> imageAvatars;
    [SerializeField] private List<Sprite> spritesAvatars;
    [SerializeField] private List<ImageElement> imageElements = new List<ImageElement>();
    [SerializeField] private Button buttonSubmitData;

    public void Initialize()
    {
        for (int i = 0; i < imageElements.Count; i++)
        {
            imageElements[i].OnChooseImage += HandlerClickToChooseAvatar;
            imageElements[i].Initialize(i);
        }

        buttonSubmitData.onClick.AddListener(HandlerClickToSubmitAvatarButton);
    }

    public void Dispose()
    {
        for (int i = 0; i < imageElements.Count; i++)
        {
            imageElements[i].OnChooseImage -= HandlerClickToChooseAvatar;
            imageElements[i].Dispose();
        }

        buttonSubmitData.onClick.RemoveListener(HandlerClickToSubmitAvatarButton);
    }

    public void Select(int index)
    {
        imageElements[index].Select();

        for (int i = 0; i < imageAvatars.Count; i++)
        {
            imageAvatars[i].sprite = spritesAvatars[index];
        }
    }

    public void Deselect(int index)
    {
        imageElements[index].Deselect();
    }

    #region Input

    private void HandlerClickToChooseAvatar(int index)
    {
        OnChooseAvatar?.Invoke(index);
    }

    private void HandlerClickToSubmitAvatarButton()
    {
        OnSubmitAvatar?.Invoke();
    }

    #endregion
}
