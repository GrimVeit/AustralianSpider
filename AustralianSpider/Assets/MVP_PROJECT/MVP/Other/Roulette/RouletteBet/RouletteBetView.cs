using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RouletteBetView : View
{
    public event Action<BetCell, Chip, Bet> OnChooseCell_Action;
    public event Action<BetCell, Chip, Bet> OnResetCell_Action;

    [SerializeField] private List<BetCell> betCells = new List<BetCell>();
    [SerializeField] private RouletteBetDisplayView displayView;
    [SerializeField] private TextMeshProUGUI textBet;
    [SerializeField] private TextMeshProUGUI textBetCounts;
    [SerializeField] private TextMeshProUGUI textWin;
    [SerializeField] private TextMeshProUGUI textProfit;

    [SerializeField] private RouletteMainResultView rouletteMainResult;

    public void Initialize()
    {
        displayView.Initialize();
        rouletteMainResult.Initialize();

        for (int i = 0; i < betCells.Count; i++)
        {
            betCells[i].OnChooseCell += OnChooseCell;
            betCells[i].OnResetCell += OnResetCell;
        }
    }

    public void Dispose()
    {
        displayView.Dispose();
        rouletteMainResult.Dispose();

        for (int i = 0; i < betCells.Count; i++)
        {
            betCells[i].OnChooseCell -= OnChooseCell;
            betCells[i].OnResetCell -= OnResetCell;
        }
    }

    public void BetDisplay(int bet)
    {
        textBet.text = bet.ToString();
        displayView.SendMoneyDisplay(bet);
    }

    public void BetCountDisplay(int count)
    {
        textBetCounts.text = count.ToString();
    }

    public void WinDisplay(int win)
    {
        textWin.text = win.ToString();
    }

    public void ProfitDisplay(int profit)
    {
        textProfit.text = profit.ToString();
    }

    public void ShowResult()
    {
        rouletteMainResult.Show();
    }

    public void HideResult()
    {
        rouletteMainResult.Hide();
    }

    #region Input

    private void OnChooseCell(BetCell betCell, Chip chip, Bet bet)
    {
        OnChooseCell_Action?.Invoke(betCell, chip, bet);
    }

    private void OnResetCell(BetCell betCell, Chip chip, Bet bet)
    {
        OnResetCell_Action?.Invoke(betCell, chip, bet);
    }



    public event Action OnStartShowResult
    {
        add { rouletteMainResult.OnStartShowResult += value; }
        remove { rouletteMainResult.OnStartShowResult -= value; }
    }

    public event Action OnFinishShowResult
    {
        add { rouletteMainResult.OnFinishShowResult += value; }
        remove { rouletteMainResult.OnFinishShowResult -= value; }
    }

    public event Action OnStartHideResult
    {
        add { rouletteMainResult.OnStartHideResult += value; }
        remove { rouletteMainResult.OnStartHideResult -= value; }
    }

    public event Action OnFinishHideResult
    {
        add { rouletteMainResult.OnFinishHideResult += value; }
        remove { rouletteMainResult.OnFinishHideResult -= value; }
    }

    #endregion
}
