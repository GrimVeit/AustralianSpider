using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class RouletteColor
{
    public bool IsActiveButton => isActiveButton;
    public string NameDesign => nameDesign;

    public event Action<int> OnChooseColorIndex;

    [SerializeField] private Button buttonChoose;
    [SerializeField] private GameObject textPurchased;
    [SerializeField] private string nameDesign;

    private int currentIndex;
    private bool isActiveButton;

    public void Initialize(int index)
    {
        currentIndex = index;

        buttonChoose.onClick.AddListener(HandlerClickToChooseButton);
    }

    public void Dispose()
    {
        buttonChoose.onClick.RemoveListener(HandlerClickToChooseButton);
    }

    public void ActivateButton()
    {
        textPurchased.SetActive(false);
        buttonChoose.gameObject.SetActive(true);
        isActiveButton = true;
    }

    public void DeactivateButton()
    {
        textPurchased.SetActive(true);
        buttonChoose.gameObject.SetActive(false);
        isActiveButton = false;
    }

    private void HandlerClickToChooseButton()
    {
        OnChooseColorIndex?.Invoke(currentIndex);
    }
}
