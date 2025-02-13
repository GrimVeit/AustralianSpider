using System;
using UnityEngine;
using UnityEngine.UI;

public class CardGameView : View
{
    public event Action OnClickIncreaseChance;
    public event Action OnClickDecreaseChance;
    public event Action OnClickContinue;

    [SerializeField] private Button increaseChanceButton;
    [SerializeField] private Button decreaseChanceButton;
    [SerializeField] private Button continueButton;

    [SerializeField] private Image increaseImage;
    [SerializeField] private Image decreaseImage;

    [SerializeField] private Sprite increaseNormalSprite;
    [SerializeField] private Sprite decreaseNormalSprite;

    [SerializeField] private Sprite increaseChooseSprite;
    [SerializeField] private Sprite decreaseChooseSprite;

    public void Initialize()
    {
        
    }

    public void Dispose()
    {
        
    }

    public void Activate()
    {
        increaseChanceButton.gameObject.SetActive(true);
        decreaseChanceButton.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(false);

        increaseChanceButton.onClick.AddListener(HandlerClickToIncreaseChance);
        decreaseChanceButton.onClick.AddListener(HandlerClickToDecreaseChance);
        continueButton.onClick.AddListener(HandlerClickToContinue);
    }

    public void Deactivate()
    {
        increaseChanceButton.gameObject.SetActive(false);
        decreaseChanceButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);

        increaseChanceButton.onClick.RemoveListener(HandlerClickToIncreaseChance);
        decreaseChanceButton.onClick.RemoveListener(HandlerClickToDecreaseChance);
        continueButton.onClick.RemoveListener(HandlerClickToContinue);
    }

    public void ChooseIncrease()
    {
        continueButton.gameObject.SetActive(true);

        increaseImage.sprite = increaseChooseSprite;
        decreaseImage.sprite = decreaseNormalSprite;
    }

    public void ChooseDecrease()
    {
        continueButton.gameObject.SetActive(true);

        increaseImage.sprite = increaseNormalSprite;
        decreaseImage.sprite = decreaseChooseSprite;
    }

    public void OnChoose(bool value)
    {
        if (value)
        {
            increaseChanceButton.gameObject.SetActive(true);
        }
        else
        {
            decreaseChanceButton.gameObject.SetActive(true);
        }
    }

    public void ResetData()
    {
        increaseImage.sprite = increaseNormalSprite;
        decreaseImage.sprite = decreaseNormalSprite;
    }


    #region

    private void HandlerClickToIncreaseChance()
    {
        OnClickIncreaseChance?.Invoke();
    }

    private void HandlerClickToDecreaseChance()
    {
        OnClickDecreaseChance?.Invoke();
    }

    private void HandlerClickToContinue()
    {
        OnClickContinue?.Invoke();
    }

    #endregion
}
