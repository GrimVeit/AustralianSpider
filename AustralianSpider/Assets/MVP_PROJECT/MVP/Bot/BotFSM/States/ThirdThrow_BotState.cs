using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdThrow_BotState : IBotState
{
    private IDiceRollProvider diceRollProvider;
    private IYatzyCombinationsProvider yatzyCombinationsProvider;

    public ThirdThrow_BotState(IDiceRollProvider diceRollProvider, IYatzyCombinationsProvider yatzyCombinationsProvider)
    {
        this.diceRollProvider = diceRollProvider;
        this.yatzyCombinationsProvider = yatzyCombinationsProvider;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    public void EnterState()
    {
        Debug.Log("¿ “»¬¿÷»ﬂ —Œ—“ŒﬂÕ»ﬂ “–≈“‹≈√Œ ¡–Œ— ¿");

        Activate();
    }

    public void ExitState()
    {

    }

    public void UpdateState()
    {

    }

    private void Activate()
    {
        Coroutines.Start(Test());
    }

    private IEnumerator Test()
    {
        yield return new WaitForSeconds(2);

        yatzyCombinationsProvider.FreezeBestCombination();

        yield return new WaitForSeconds(1);

        yatzyCombinationsProvider.SubmitFreezeCombination();
        diceRollProvider.Reload();
    }
}
