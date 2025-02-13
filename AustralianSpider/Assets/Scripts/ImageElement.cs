using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageElement : MonoBehaviour
{
    public event Action<int> OnChooseImage;

    [SerializeField] private Button buttonChoose;
    [SerializeField] private Image imageStatus;
    [SerializeField] private Sprite spriteSelect;
    [SerializeField] private Sprite spriteUnselect;

    private int indexImage;


    public void Initialize(int index)
    {
        indexImage = index;

        buttonChoose.onClick.AddListener(HandlerClickToImage);
    }

    public void Dispose()
    {
        buttonChoose.onClick.RemoveListener(HandlerClickToImage);
    }

    public void Select()
    {
        imageStatus.sprite = spriteSelect;
    }

    public void Deselect()
    {
        imageStatus.sprite = spriteUnselect;
    }

    #region Input

    private void HandlerClickToImage()
    {
        OnChooseImage?.Invoke(indexImage);
    }

    #endregion
}
