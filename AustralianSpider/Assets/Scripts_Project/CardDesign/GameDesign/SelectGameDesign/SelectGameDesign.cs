using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectGameDesign : MonoBehaviour
{
    public int ID => id;

    [SerializeField] private Button buttonSelect;
    [SerializeField] private Image imageDesign;
    [SerializeField] private GameObject objectSelect;
    [SerializeField] private GameObject objectClose;

    private GameDesign currentDesign;
    private int id => currentDesign.ID;

    public void Initialize()
    {
        buttonSelect.onClick.AddListener(HandleChooseGameDesign);
    }

    public void Dispose()
    {
        buttonSelect.onClick.RemoveListener(HandleChooseGameDesign);
    }

    public void SetData(GameDesign design)
    {
        currentDesign = design;
        imageDesign.sprite = currentDesign.SpriteDesignDescription;
    }

    public void OpenDesign()
    {
        buttonSelect.enabled = true;

        objectClose.SetActive(false);
    }

    public void CloseDesign()
    {
        buttonSelect.enabled = true;

        objectClose.SetActive(true);
    }

    public void SelectDesign()
    {
        objectSelect.SetActive(true);
    }

    public void DeselectDesign()
    {
        objectSelect.SetActive(false);
    }

    #region Input

    public event Action<int> OnChooseGameDesign;

    private void HandleChooseGameDesign()
    {
        OnChooseGameDesign?.Invoke(id);
    }

    #endregion
}
