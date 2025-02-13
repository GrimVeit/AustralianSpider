using System;
using System.Collections.Generic;

public class BotStateMachine
{
    private Dictionary<Type, IBotState> botStates = new Dictionary<Type, IBotState>();

    private IBotState currentBotState;


    private IDiceRollProvider rollProvider;
    private IYatzyCombinationsProvider combinationsProvider;

    public BotStateMachine(IDiceRollProvider rollProvider, IYatzyCombinationsProvider combinationsProvider)
    {
        this.rollProvider = rollProvider;
        this.combinationsProvider = combinationsProvider;
    }

    public void Initialize()
    {
        botStates[typeof(HoldOn_BotState)] = new HoldOn_BotState();
        botStates[typeof(FirstThrow_BotState)] = new FirstThrow_BotState(rollProvider, combinationsProvider);
        botStates[typeof(SecondThrow_BotState)] = new SecondThrow_BotState(rollProvider, combinationsProvider);
        botStates[typeof(ThirdThrow_BotState)] = new ThirdThrow_BotState(rollProvider, combinationsProvider);

        rollProvider.OnStartRoll += OnStartRoll;
        rollProvider.OnGetAllDiceValues += OnEndRoll;

        GetBotState<HoldOn_BotState>().Initialize();
        GetBotState<FirstThrow_BotState>().Initialize();
        GetBotState<SecondThrow_BotState>().Initialize();
        GetBotState<ThirdThrow_BotState>().Initialize();
    }

    public void Dispose()
    {
        rollProvider.OnStartRoll -= OnStartRoll;
        rollProvider.OnGetAllDiceValues -= OnEndRoll;

        GetBotState<HoldOn_BotState>().Dispose();
        GetBotState<FirstThrow_BotState>().Dispose();
        GetBotState<SecondThrow_BotState>().Dispose();
        GetBotState<ThirdThrow_BotState>().Dispose();
    }

    private void OnStartRoll()
    {
        SetStateMachine(GetBotState<HoldOn_BotState>());
    }

    private void OnEndRoll(int attempt, int[] nones)
    {
        switch (attempt)
        {
            case 0:
                SetStateMachine(GetBotState<ThirdThrow_BotState>());
                break;
            case 1:
                SetStateMachine(GetBotState<SecondThrow_BotState>());
                break;
            case 2:
                SetStateMachine(GetBotState<FirstThrow_BotState>());
                break;
        }
    }


    public void SetStateMachine(IBotState botState)
    {
        currentBotState?.ExitState();

        currentBotState = botState;
        currentBotState.EnterState();
    }

    public IBotState GetBotState<T>() where T : IBotState
    {
        return botStates[typeof(T)];
    }
}
