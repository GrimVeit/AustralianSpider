using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceEffectModel_First : IDiceEffectModel
{
    private IParticleEffectProvider particleEffectProvider;

    public DiceEffectModel_First(IParticleEffectProvider particleEffectProvider)
    {
        this.particleEffectProvider = particleEffectProvider;
    }

    public void SetDiceIndex(int index)
    {
        particleEffectProvider.Play("YellowDice_" + index);
    }
}
