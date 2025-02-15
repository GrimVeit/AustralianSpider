using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectFaceCardDesign : MonoBehaviour
{
    public int ID => id;

    [SerializeField] private Button buttonSelect;
    [SerializeField] private Image imageDesign;
    [SerializeField] private GameObject objectSelect;
    [SerializeField] private GameObject objectClose;

    private FaceCardDesign currentDesign;
    private int id => currentDesign.ID;

    public void Initialize()
    {
        buttonSelect.onClick.AddListener(HandleChooseFaceCardDesign);
    }

    public void Dispose()
    {
        buttonSelect.onClick.RemoveListener(HandleChooseFaceCardDesign);
    }

    public void SetData(FaceCardDesign design)
    {
        currentDesign = design;
        imageDesign.sprite = currentDesign.SpriteDesign;
    }

    public void OpenDesign()
    {
        buttonSelect.enabled = true;

        objectClose.SetActive(false);
    }

    public void CloseDesign()
    {
        buttonSelect.enabled = false;

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

    public event Action<int> OnChooseFaceCardDesign;

    private void HandleChooseFaceCardDesign()
    {
        OnChooseFaceCardDesign?.Invoke(id);
    }

    #endregion
}
