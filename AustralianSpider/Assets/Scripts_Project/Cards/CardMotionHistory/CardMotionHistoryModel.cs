using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMotionHistoryModel
{
    public event Action OnActivate;
    public event Action OnDeactivate;
    public event Action<CardInteractive, List<CardInteractive>, Column> OnRemoveLastMotion;

    private List<CardMotionHistory> cardMotionHistories = new List<CardMotionHistory>();

    public void AddMotion(CardInteractive cardInteractive, Column column)
    {
        Debug.Log(cardInteractive.Value + "//" + column.name);

        cardMotionHistories.Add(new CardMotionHistory(cardInteractive, column));

        OnActivate?.Invoke();
    }

    public void ReturmLastMotion()
    {
        Debug.Log("уси");

        if(cardMotionHistories.Count > 0)
        {
            Debug.Log("уси 2");

            var motion = cardMotionHistories[^1];

            OnRemoveLastMotion?.Invoke(motion.cardInteractive, motion.cardChildrens, motion.column);

            cardMotionHistories.Remove(motion);
        }

        if(cardMotionHistories.Count == 0)
        {
            OnDeactivate?.Invoke();
        }
    }

    public void ClearHistory()
    {
        cardMotionHistories.Clear();

        OnDeactivate?.Invoke();
    }
}

public class CardMotionHistory
{
    public CardInteractive cardInteractive;
    public Column column;
    public List<CardInteractive> cardChildrens;

    public CardMotionHistory(CardInteractive cardInteractive, Column column)
    {
        this.cardInteractive = cardInteractive;
        this.column = column;
        cardChildrens = cardInteractive.Children;
    }
}