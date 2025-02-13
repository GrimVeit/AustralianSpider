using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YatzyEffectModel_Second : IYatzyEffectModel
{
    private IParticleEffectProvider particleEffectProvider;
    public YatzyEffectModel_Second(IParticleEffectProvider particleEffectProvider)
    {
        this.particleEffectProvider = particleEffectProvider;
    }

    public void SetYatzyCombinationIndex(int index)
    {
        particleEffectProvider.Play("WhiteCombination_" + index);
    }
}
