using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceEffectModel_Second : IDiceEffectModel
{
    private IParticleEffectProvider particleEffectProvider;

    public DiceEffectModel_Second(IParticleEffectProvider particleEffectProvider)
    {
        this.particleEffectProvider = particleEffectProvider;
    }

    public void SetDiceIndex(int index)
    {
        particleEffectProvider.Play("WhiteDice_" + index);
    }
}
