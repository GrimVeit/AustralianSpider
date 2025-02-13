using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YatzyEffectModel_First : IYatzyEffectModel
{
    private IParticleEffectProvider particleEffectProvider;
    public YatzyEffectModel_First(IParticleEffectProvider particleEffectProvider)
    {
        this.particleEffectProvider = particleEffectProvider;
    }

    public void SetYatzyCombinationIndex(int index)
    {
        particleEffectProvider.Play("GreenCombination_" + index);
    }
}
