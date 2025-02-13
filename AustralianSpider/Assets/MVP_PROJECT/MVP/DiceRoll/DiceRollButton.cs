using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollButton : MonoBehaviour
{
    [SerializeField] private Button buttonDiceRoll;
    [SerializeField] private TextMeshProUGUI textAttempts;
    [SerializeField] private Image imageButton;
    [SerializeField] private Sprite spriteActive;
    [SerializeField] private Sprite spriteDeactive;

    public void Initialize()
    {
        buttonDiceRoll.onClick.AddListener(HandlerClickToDiceRollButton);
    }

    public void Dispose()
    {
        buttonDiceRoll.onClick.RemoveListener(HandlerClickToDiceRollButton);
    }

    public void ActivateButton()
    {
        buttonDiceRoll.enabled = true;
        imageButton.sprite = spriteActive;
    }

    public void DeactivateButton()
    {
        buttonDiceRoll.enabled = false;
        imageButton.sprite = spriteDeactive;
    }

    public void ChangeAttempts(int attempts)
    {
        textAttempts.text = attempts.ToString();
    }

    #region Input

    public event Action OnClickToDiceRollButton;

    private void HandlerClickToDiceRollButton()
    {
        OnClickToDiceRollButton?.Invoke();
    }

    #endregion
}
