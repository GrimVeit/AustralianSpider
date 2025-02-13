using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Cards/NewCard")]
public class Card : ScriptableObject
{
    [SerializeField] private int cardId;
    [SerializeField] private CardType cardType;
    [SerializeField] private Sprite sprite;

    public int CardId => cardId;
    public CardType CardType => cardType;
    public Sprite Sprite => sprite;
}
