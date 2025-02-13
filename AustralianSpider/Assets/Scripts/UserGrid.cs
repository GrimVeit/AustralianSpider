using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserGrid : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nicknameText;
    [SerializeField] private TextMeshProUGUI recordText;
    [SerializeField] private Image avatarImage;

    public string Nickname { get; private set; }

    public void SetData(string nickname, string record, Sprite sprite)
    {
        this.Nickname = nickname;
        nicknameText.text = Nickname;
        recordText.text = record;
        avatarImage.sprite = sprite;
    }

    public void SetAvatar(Sprite sprite)
    {
        avatarImage.sprite = sprite;
    }
}
