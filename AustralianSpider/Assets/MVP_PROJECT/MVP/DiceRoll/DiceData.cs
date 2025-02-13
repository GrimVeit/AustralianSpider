using UnityEngine;

[System.Serializable]
public class DiceData
{
    [SerializeField] private Sprite spriteDice;
    [SerializeField] private int number;
    private bool frozen;

    public DiceData(bool frozen)
    {
        this.frozen = frozen;
    }

    public Sprite DiceSprite => spriteDice;
    public int Number => number;
    public bool Frozen => frozen;

    public void SetFrozen(bool state)
    {
        this.frozen = state;
    }
}
