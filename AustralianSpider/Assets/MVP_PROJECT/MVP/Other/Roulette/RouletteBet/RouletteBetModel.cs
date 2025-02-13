using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteBetModel
{
    public event Action<Chip> OnNoneRetractedChip;
    public event Action<Chip> OnDestroyedChip;

    public event Action OnShowResult;
    public event Action OnHideResult;

    public event Action<int> OnChangeBet;
    public event Action<int> OnChangeWin;
    public event Action<int> OnChangeBetCount;
    public event Action<int> OnChangeProfit;

    public event Action OnStartShowResult;
    public event Action OnFinishShowResult;
    public event Action OnStartHideResult;
    public event Action OnFinishHideResult;

    private Dictionary<Chip, BetCell> usedCells = new Dictionary<Chip, BetCell>();

    private IMoneyProvider moneyProvider;

    private int totalBet;
    private int totalWin;
    private int totalProfit;
    private int totalBetCount;

    private RouletteSlotValue rouletteSlotValue;

    public RouletteBetModel(IMoneyProvider moneyProvider)
    {
        this.moneyProvider = moneyProvider;
    }

    public void ChooseCell(BetCell betCell, Chip chip, Bet bet)
    {
        int betValue = chip.ChipData.Nominal;
        totalBet += betValue;
        moneyProvider.SendMoney(-betValue);
        usedCells.Add(chip, betCell);

        totalBetCount += 1;

        OnChangeBet?.Invoke(totalBet);
        OnChangeBetCount?.Invoke(totalBetCount);
    }

    public void ResetCell(BetCell betCell, Chip chip, Bet bet)
    {
        int betValue = chip.ChipData.Nominal;
        totalBet -= betValue;
        moneyProvider.SendMoney(betValue);
        usedCells.Remove(chip);

        totalBetCount -= 1;

        OnChangeBet?.Invoke(totalBet);
        OnChangeBetCount?.Invoke(totalBetCount);
    }

    public void GetRouletteSlotValue(RouletteSlotValue rouletteSlotValue)
    {
        this.rouletteSlotValue = rouletteSlotValue;
    }

    public void SearchWin()
    {
        totalWin = 0;
        totalProfit = -totalBet;
        foreach (var cell in usedCells)
        {
            if (cell.Value.Bet.Numbers.Contains(rouletteSlotValue.RouletteNumber.Number))
            {
                Debug.Log(cell.Key.ChipData.Nominal + "//" + cell.Value.Bet.MultiplyPayout);
                totalWin += cell.Key.ChipData.Nominal * cell.Value.Bet.MultiplyPayout;
            }
        }
        totalProfit += totalWin;

        OnChangeWin?.Invoke(totalWin);
        OnChangeProfit?.Invoke(totalProfit);
    }

    public void ReturnChips()
    {
        foreach (var cell in usedCells)
        {
            Debug.Log(usedCells.Count);
            Chip chip = cell.Key;
            if (cell.Value.Bet.Numbers.Contains(rouletteSlotValue.RouletteNumber.Number))
            {
                OnNoneRetractedChip?.Invoke(chip);
            }
            else
            {
                OnDestroyedChip?.Invoke(chip);
            }

            Debug.Log(usedCells.Count);
        }

        usedCells.Clear();

        totalBetCount = 0;
        OnChangeBetCount?.Invoke(totalBetCount);

        totalBet = 0;
        OnChangeBet?.Invoke(totalBet);

        if(totalProfit > 0)
           moneyProvider.SendMoney(totalWin);
    }

    public void ShowResult()
    {
        Coroutines.Start(TimerHideResult());

        OnShowResult?.Invoke();
    }

    private IEnumerator TimerHideResult()
    {
        yield return new WaitForSeconds(8);
        HideResult();
    }

    public void HideResult()
    {
        OnHideResult?.Invoke();
    }

    public void StartShowResult()
    {
        OnStartShowResult?.Invoke();
    }

    public void FinishShowResult()
    {
        OnFinishShowResult?.Invoke();
    }

    public void StartHideResult()
    {
        OnStartHideResult?.Invoke();
    }

    public void FinishHideResult()
    {
        OnFinishHideResult?.Invoke();
    }
}
