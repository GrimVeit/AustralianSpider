using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollView : View, IIdentify
{
    public string GetID() => idComponent;

    [SerializeField] private string idComponent;

    public event Action<int, DiceData> OnStoppedDice_Action;

    [SerializeField] private List<Dice> dices = new List<Dice>();
    [SerializeField] private List<DiceRollButton> diceRollButtons = new List<DiceRollButton>();

    public void Initialize()
    {
        for (int i = 0; i < dices.Count; i++)
        {
            dices[i].OnStopRotated += OnStoppedDice;
            dices[i].OnClickToDice += HandlerClickToDice;
            dices[i].Initialize(i);
        }

        for (int i = 0; i < diceRollButtons.Count; i++)
        {
            diceRollButtons[i].OnClickToDiceRollButton += HandlerClickToRollButton;
            diceRollButtons[i].Initialize();
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < dices.Count; i++)
        {
            dices[i].OnStopRotated -= OnStoppedDice;
            dices[i].OnClickToDice -= HandlerClickToDice;
            dices[i].Dispose();
        }

        for (int i = 0; i < diceRollButtons.Count; i++)
        {
            diceRollButtons[i].OnClickToDiceRollButton -= HandlerClickToRollButton;
            diceRollButtons[i].Dispose();
        }
    }

    public void Roll(int[] indexes)
    {
        for (int i = 0; i < indexes.Length; i++)
        {
            //Debug.Log("Кручение кубика под индексом - " + indexes[i]);
            dices[indexes[i]].Roll();
        }
    }

    public void ActivateButton()
    {
        for (int i = 0; i < diceRollButtons.Count; i++)
        {
            diceRollButtons[i].ActivateButton();
        }
    }

    public void DeactivateButton()
    {
        for (int i = 0; i < diceRollButtons.Count; i++)
        {
            diceRollButtons[i].DeactivateButton();
        }
    }

    public void OnChangeAttempts(int count)
    {
        for (int i = 0; i < diceRollButtons.Count; i++)
        {
            diceRollButtons[i].ChangeAttempts(count);
        }
    }

    public void FreezeDice(int index)
    {
        dices[index].Freese();
    }

    public void UnfreeseDice(int index)
    {
        dices[index].Unfreese();
    }

    #region Input

    public event Action OnClickToRollButton;
    public event Action<int> OnClickToDice;

    private void HandlerClickToRollButton()
    {
        OnClickToRollButton?.Invoke();
    }

    private void OnStoppedDice(int index, DiceData diceData)
    {
        OnStoppedDice_Action?.Invoke(index, diceData);
    }

    private void HandlerClickToDice(int index)
    {
        OnClickToDice?.Invoke(index);
    }

    #endregion
}
