using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMotionHistoryModel
{
    public event Action OnActivate;
    public event Action OnDeactivate;
    public event Action<CardInteractive, List<CardInteractive>, Column, bool> OnRemoveLastMotion;

    private List<CardMotionHistory> cardMotionHistories = new List<CardMotionHistory>();

    public void AddMotion(CardInteractive cardInteractive, Column column, bool isActiveHigherCard)
    {
        Debug.Log(cardInteractive.Value + "//" + column.name);

        cardMotionHistories.Add(new CardMotionHistory(cardInteractive, column, isActiveHigherCard));

        OnActivate?.Invoke();
    }

    public void ReturmLastMotion()
    {
        Debug.Log("уси");

        if(cardMotionHistories.Count > 0)
        {
            Debug.Log("уси 2");

            var motion = cardMotionHistories[^1];

            OnRemoveLastMotion?.Invoke(motion.cardInteractive, motion.cardChildrens, motion.column, motion.isActiveHigherCard);

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
    public bool isActiveHigherCard;

    public CardMotionHistory(CardInteractive cardInteractive, Column column, bool isActiveHigherCard)
    {
        this.cardInteractive = cardInteractive;
        this.column = column;
        this.isActiveHigherCard = isActiveHigherCard;
        cardChildrens = cardInteractive.Children;
    }
}