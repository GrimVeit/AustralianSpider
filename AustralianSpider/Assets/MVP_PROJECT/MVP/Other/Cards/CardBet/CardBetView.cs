using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardBetView : View
{
    public event Action OnIncreaseBet;
    public event Action OnDeacreaseBet;
    public event Action OnContinue;


    [SerializeField] private GameObject displayBet;
    [SerializeField] private Button increaseBetButton;
    [SerializeField] private Button decreaseBetButton;
    [SerializeField] private TextMeshProUGUI textBet;
    [SerializeField] private Button continueButton;


    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    public void Activate()
    {
        displayBet.SetActive(true);
        continueButton.gameObject.SetActive(true);

        increaseBetButton.onClick.AddListener(HandlerClickToIncreaseButton);
        decreaseBetButton.onClick.AddListener(HandlerClickToDecreaseButton);
        continueButton.onClick.AddListener(HandlerClickToContinue);
    }

    public void Deactivate()
    {
        increaseBetButton.onClick.RemoveListener(HandlerClickToIncreaseButton);
        decreaseBetButton.onClick.RemoveListener(HandlerClickToDecreaseButton);
        continueButton.onClick.RemoveListener(HandlerClickToContinue);

        increaseBetButton.gameObject.SetActive(false);
        decreaseBetButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
    }

    public void DownNormalBet()
    {
        decreaseBetButton.gameObject.SetActive(false);
    }

    public void UpNormalBet()
    {
        increaseBetButton.gameObject.SetActive(false);
    }

    public void DisplayBet(int bet)
    {
        textBet.text = bet.ToString();

        increaseBetButton.gameObject.SetActive(true);
        decreaseBetButton.gameObject.SetActive(true);
    }

    #region Input

    private void HandlerClickToIncreaseButton()
    {
        OnIncreaseBet?.Invoke();
    }

    private void HandlerClickToDecreaseButton()
    {
        OnDeacreaseBet?.Invoke();
    }

    private void HandlerClickToContinue()
    {
        OnContinue?.Invoke();
    }

    #endregion
}
