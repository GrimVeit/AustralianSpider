using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceEffectPresenter
{
    public IDiceEffectModel diceEffectModel;

    public DiceEffectPresenter(IDiceEffectModel diceEffectModel)
    {
        this.diceEffectModel = diceEffectModel;
    }

    public void SetDiceIndex(int index)
    {
        diceEffectModel.SetDiceIndex(index);
    }
}

public interface IDiceEffectModel
{
    public void SetDiceIndex(int index);
}
