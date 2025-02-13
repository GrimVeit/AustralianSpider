using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YatzyEffectPresenter
{
    public IYatzyEffectModel yatzyEffectModel;

    public YatzyEffectPresenter(IYatzyEffectModel yatzyEffectModel)
    {
        this.yatzyEffectModel = yatzyEffectModel;
    }

    public void SetYatzyCombinationIndex(int index)
    {
        yatzyEffectModel.SetYatzyCombinationIndex(index);
    }
}

public interface IYatzyEffectModel
{
    public void SetYatzyCombinationIndex(int index);
}
