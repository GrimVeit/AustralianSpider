using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bet
{
    [SerializeField] private BetType type;
    [SerializeField] private List<int> numbers = new List<int>();
    [SerializeField] private int multiplyPayout;

    public BetType Type => type;
    public List<int> Numbers => numbers;
    public int MultiplyPayout => multiplyPayout;
}

public enum BetType
{
    SingleNumber, 
    Even,
    Odd,
    Red,
    Black,
    FirstColumn,
    SecondColumn,
    ThirdColumn,
    FirstRow,
    SecondRow,
    ThirdRow,
    FirstHalf,
    SecondHalf
}
