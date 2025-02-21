using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMotionHistoryModel
{
    public event Action<CardInteractive, List<CardInteractive>, Column> OnRemoveLastMotion;

    private List<CardMotionHistory> cardMotionHistories = new List<CardMotionHistory>();

    public void AddMotion(CardInteractive cardInteractive, Column column)
    {
        Debug.Log(cardInteractive.Value + "//" + column.name);

        cardMotionHistories.Add(new CardMotionHistory(cardInteractive, column));
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
    }

    public void ClearHistory()
    {
        cardMotionHistories.Clear();
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