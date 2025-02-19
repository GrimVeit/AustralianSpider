using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectGameType : MonoBehaviour
{
    public int ID => id;

    [SerializeField] private int id;
    [SerializeField] private Button buttonSelect;
    [SerializeField] private Image imageDesign;
    [SerializeField] private Sprite spriteSelect;
    [SerializeField] private Sprite spriteDeselect;

    public void Initialize()
    {
        buttonSelect.onClick.AddListener(HandleChooseGameType);
    }

    public void Dispose()
    {
        buttonSelect.onClick.RemoveListener(HandleChooseGameType);
    }

    public void SelectDesign()
    {
        imageDesign.sprite = spriteSelect;
    }

    public void DeselectDesign()
    {
        imageDesign.sprite = spriteDeselect;
    }

    #region Input

    public event Action<int> OnChooseGameType;

    private void HandleChooseGameType()
    {
        OnChooseGameType?.Invoke(id);
    }

    #endregion
}
