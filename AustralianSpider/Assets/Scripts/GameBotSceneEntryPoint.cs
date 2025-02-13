using System;
using UnityEngine;

public class GameBotSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private UIGameBotRoot menuRootPrefab;

    private UIGameBotRoot sceneRoot;
    private ViewContainer viewContainer;

    private ParticleEffectPresenter particleEffectPresenter;
    private SoundPresenter soundPresenter;
    private BankPresenter bankPresenter;

    private DiceRollPresenter diceRollPresenter_Me;
    private YatzyCombinationPresenter yatzyCombinationPresenter_Me;

    private DiceRollPresenter diceRollPresenter_Bot;
    private YatzyCombinationPresenter yatzyCombinationPresenter_Bot;
    private PlayerPresenter playerPresenter;

    private GameSessionPresenter gameSessionPresenter;
    private BotPresenter botPresenter;

    private YatzyEffectPresenter yatzyEffectPresenter_Me;
    private YatzyEffectPresenter yatzyEffectPresenter_Bot;
    private DiceEffectPresenter diceEffectPresenter_Me;
    private DiceEffectPresenter diceEffectPresenter_Bot;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = Instantiate(menuRootPrefab);

        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        soundPresenter = new SoundPresenter
            (new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS),
            viewContainer.GetView<SoundView>());
        soundPresenter.Initialize();

        particleEffectPresenter = new ParticleEffectPresenter
            (new ParticleEffectModel(),
            viewContainer.GetView<ParticleEffectView>());
        particleEffectPresenter.Initialize();

        gameSessionPresenter = new GameSessionPresenter
            (new GameSessionModel(), 
            viewContainer.GetView<GameSessionView>());
        gameSessionPresenter.Initialize();



        diceRollPresenter_Me = new DiceRollPresenter
            (new DiceRollModel(5, 3, soundPresenter),
            viewContainer.GetView<DiceRollView>("Me"));
        diceRollPresenter_Me.Initialize();

        yatzyCombinationPresenter_Me = new YatzyCombinationPresenter
            (new YatzyCombinationModel(13, soundPresenter),
            viewContainer.GetView<YatzyCombinationView>("Me"));
        yatzyCombinationPresenter_Me.Initialize();




        diceRollPresenter_Bot = new DiceRollPresenter
            (new DiceRollModel(5, 3, soundPresenter),
            viewContainer.GetView<DiceRollView>("Bot"));
        diceRollPresenter_Bot.Initialize();

        yatzyCombinationPresenter_Bot = new YatzyCombinationPresenter
            (new YatzyCombinationModel(13, soundPresenter),
            viewContainer.GetView<YatzyCombinationView>("Bot"));
        yatzyCombinationPresenter_Bot.Initialize();



        playerPresenter = new PlayerPresenter
            (new PlayerModel(PlayerPrefsKeys.NICKNAME, PlayerPrefsKeys.IMAGE_INDEX), 
            viewContainer.GetView<PlayerView>());
        playerPresenter.Initialize();

        botPresenter = new BotPresenter
            (new BotModel(), 
            viewContainer.GetView<BotView>(), 
            new BotStateMachine(diceRollPresenter_Bot, yatzyCombinationPresenter_Bot));
        botPresenter.Initialize();

        yatzyEffectPresenter_Me = new YatzyEffectPresenter(new YatzyEffectModel_First(particleEffectPresenter));
        yatzyEffectPresenter_Bot = new YatzyEffectPresenter(new YatzyEffectModel_Second(particleEffectPresenter));
        diceEffectPresenter_Me = new DiceEffectPresenter(new DiceEffectModel_First(particleEffectPresenter));
        diceEffectPresenter_Bot = new DiceEffectPresenter(new DiceEffectModel_Second(particleEffectPresenter));

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Initialize();

        ActivateEvents();

        sceneRoot.Activate();
    }

    private void ActivateEvents()
    {
        ActivateTransitionsSceneEvents();


        //Me
        diceRollPresenter_Me.OnGetAllDiceValues += yatzyCombinationPresenter_Me.SetNumbersCombination;
        diceRollPresenter_Me.OnStartRoll += yatzyCombinationPresenter_Me.Deactivate;
        diceRollPresenter_Me.OnStopRoll += yatzyCombinationPresenter_Me.Activate;
        diceRollPresenter_Me.OnStartRoll += diceRollPresenter_Me.DeactivateFreezeToggle;
        diceRollPresenter_Me.OnStopRoll += diceRollPresenter_Me.ActivateFreezeToggle;

        yatzyCombinationPresenter_Me.OnFreezeYatzyCombination += gameSessionPresenter.ChangeToSecondUser;
        yatzyCombinationPresenter_Me.OnFreezeYatzyCombination += diceRollPresenter_Me.Reload;
        yatzyCombinationPresenter_Me.OnFreezeYatzyCombination += yatzyCombinationPresenter_Me.Deactivate;
        yatzyCombinationPresenter_Me.OnFreezeYatzyCombination += diceRollPresenter_Me.DeactivateFreezeToggle;



        //Bot
        gameSessionPresenter.OnChangedToSecondUser += diceRollPresenter_Bot.StartRoll;
        diceRollPresenter_Bot.OnStartRoll += yatzyCombinationPresenter_Bot.Deactivate;
        diceRollPresenter_Bot.OnStopRoll += yatzyCombinationPresenter_Bot.Activate;
        yatzyCombinationPresenter_Bot.OnFreezeYatzyCombination += gameSessionPresenter.ChangeToFirstUser;
        diceRollPresenter_Bot.OnGetAllDiceValues += yatzyCombinationPresenter_Bot.SetNumbersCombination;
        diceRollPresenter_Bot.OnStartRoll += diceRollPresenter_Bot.DeactivateFreezeToggle;
        diceRollPresenter_Bot.OnStopRoll += diceRollPresenter_Bot.ActivateFreezeToggle;
        yatzyCombinationPresenter_Bot.OnFreezeYatzyCombination += yatzyCombinationPresenter_Bot.Deactivate;
        yatzyCombinationPresenter_Bot.OnFreezeYatzyCombination += diceRollPresenter_Bot.DeactivateFreezeToggle;

        //diceRollPresenter_Bot.OnStopRoll += yatzyCombinationPresenter_Bot.FreezeBestCombination;
        //yatzyCombinationPresenter_Bot.OnSelectCombination += yatzyCombinationPresenter_Bot.SubmitFreezeCombination;
        //yatzyCombinationPresenter_Bot.OnFreezeYatzyCombination += diceRollPresenter_Bot.Reload;



        yatzyCombinationPresenter_Me.OnSelectCombination_Index += yatzyEffectPresenter_Me.SetYatzyCombinationIndex;
        yatzyCombinationPresenter_Bot.OnSelectCombination_Index += yatzyEffectPresenter_Bot.SetYatzyCombinationIndex;
        diceRollPresenter_Me.OnFreezeDice_Index += diceEffectPresenter_Me.SetDiceIndex;
        diceRollPresenter_Bot.OnFreezeDice_Index += diceEffectPresenter_Bot.SetDiceIndex;
    }

    private void DeactivateEvents()
    {
        DeactivateTransitionsSceneEvents();

        //Me
        diceRollPresenter_Me.OnGetAllDiceValues -= yatzyCombinationPresenter_Me.SetNumbersCombination;
        diceRollPresenter_Me.OnStartRoll -= yatzyCombinationPresenter_Me.Deactivate;
        diceRollPresenter_Me.OnStopRoll -= yatzyCombinationPresenter_Me.Activate;
        diceRollPresenter_Me.OnStartRoll -= diceRollPresenter_Me.DeactivateFreezeToggle;
        diceRollPresenter_Me.OnStopRoll -= diceRollPresenter_Me.ActivateFreezeToggle;

        yatzyCombinationPresenter_Me.OnFreezeYatzyCombination -= gameSessionPresenter.ChangeToSecondUser;
        yatzyCombinationPresenter_Me.OnFreezeYatzyCombination -= diceRollPresenter_Me.Reload;
        yatzyCombinationPresenter_Me.OnFreezeYatzyCombination -= yatzyCombinationPresenter_Me.Deactivate;
        yatzyCombinationPresenter_Me.OnFreezeYatzyCombination -= diceRollPresenter_Me.DeactivateFreezeToggle;



        //Bot
        yatzyCombinationPresenter_Bot.OnFreezeYatzyCombination -= gameSessionPresenter.ChangeToFirstUser;
        diceRollPresenter_Bot.OnGetAllDiceValues -= yatzyCombinationPresenter_Bot.SetNumbersCombination;
        diceRollPresenter_Bot.OnStartRoll -= yatzyCombinationPresenter_Bot.Deactivate;
        diceRollPresenter_Bot.OnStopRoll -= yatzyCombinationPresenter_Bot.Activate;
        diceRollPresenter_Bot.OnStartRoll -= diceRollPresenter_Bot.DeactivateFreezeToggle;
        diceRollPresenter_Bot.OnStopRoll -= diceRollPresenter_Bot.ActivateFreezeToggle;

        yatzyCombinationPresenter_Bot.OnFreezeYatzyCombination -= diceRollPresenter_Bot.Reload;
        yatzyCombinationPresenter_Bot.OnFreezeYatzyCombination -= yatzyCombinationPresenter_Bot.Deactivate;
        yatzyCombinationPresenter_Bot.OnFreezeYatzyCombination -= diceRollPresenter_Bot.DeactivateFreezeToggle;
        yatzyCombinationPresenter_Me.OnSelectCombination_Index -= yatzyEffectPresenter_Me.SetYatzyCombinationIndex;
        yatzyCombinationPresenter_Bot.OnSelectCombination_Index -= yatzyEffectPresenter_Bot.SetYatzyCombinationIndex;
        diceRollPresenter_Me.OnFreezeDice_Index -= diceEffectPresenter_Me.SetDiceIndex;
        diceRollPresenter_Bot.OnFreezeDice_Index -= diceEffectPresenter_Bot.SetDiceIndex;
    }

    private void ActivateTransitionsSceneEvents()
    {
        sceneRoot.OnClickToGoMainMenuFromMainPanel += HandleGoToMainMenu;
        sceneRoot.OnClickToGoMainMenuFromWinFinishPanel += HandleGoToMainMenu;
        sceneRoot.OnClickToGoSoloGameFromWinFinishPanel += HandleGoToBotGame;
        sceneRoot.OnClickToGoMainMenuFromLoseFinishPanel += HandleGoToMainMenu;
        sceneRoot.OnClickToGoSoloGameFromLoseFinishPanel += HandleGoToBotGame;


        diceRollPresenter_Me.OnLoseFirstAttempt += sceneRoot.OpenPlayRollPanel_Me;
        diceRollPresenter_Bot.OnLoseFirstAttempt += sceneRoot.OpenPlayRollPanel_Bot;

        gameSessionPresenter.OnChangedToFirstUser += sceneRoot.OpenGamePanel_Me;
        gameSessionPresenter.OnChangedToFirstUser += sceneRoot.OpenRollPanel_Me;

        gameSessionPresenter.OnChangedToSecondUser += sceneRoot.OpenGamePanel_Bot;
        gameSessionPresenter.OnChangedToSecondUser += sceneRoot.OpenRollPanel_Bot;

        gameSessionPresenter.OnWinFirstUser += sceneRoot.OpenWinFinishPanel;
        gameSessionPresenter.OnWinSecondUser += sceneRoot.OpenLoseFinishPanel;
    }

    private void DeactivateTransitionsSceneEvents()
    {
        sceneRoot.OnClickToGoMainMenuFromMainPanel -= HandleGoToMainMenu;
        sceneRoot.OnClickToGoMainMenuFromWinFinishPanel -= HandleGoToMainMenu;
        sceneRoot.OnClickToGoSoloGameFromWinFinishPanel -= HandleGoToBotGame;
        sceneRoot.OnClickToGoMainMenuFromLoseFinishPanel -= HandleGoToMainMenu;
        sceneRoot.OnClickToGoSoloGameFromLoseFinishPanel -= HandleGoToBotGame;

        diceRollPresenter_Me.OnLoseFirstAttempt -= sceneRoot.OpenPlayRollPanel_Me;
        diceRollPresenter_Bot.OnLoseFirstAttempt -= sceneRoot.OpenPlayRollPanel_Bot;

        gameSessionPresenter.OnChangedToFirstUser -= sceneRoot.OpenGamePanel_Me;
        gameSessionPresenter.OnChangedToFirstUser -= sceneRoot.OpenRollPanel_Me;

        gameSessionPresenter.OnChangedToSecondUser -= sceneRoot.OpenGamePanel_Bot;
        gameSessionPresenter.OnChangedToSecondUser -= sceneRoot.OpenRollPanel_Bot;

        gameSessionPresenter.OnWinFirstUser -= sceneRoot.OpenWinFinishPanel;
        gameSessionPresenter.OnWinSecondUser -= sceneRoot.OpenLoseFinishPanel;
    }

    private void Deactivate()
    {
        soundPresenter?.Dispose();
        sceneRoot.Deactivate();
    }

    private void Dispose()
    {
        DeactivateEvents();

        sceneRoot?.Dispose();
        particleEffectPresenter?.Dispose();
        bankPresenter?.Dispose();

        diceRollPresenter_Me?.Dispose();
        yatzyCombinationPresenter_Me?.Dispose();
        playerPresenter?.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region Input actions

    public event Action OnGoToMainMenu;
    public event Action OnGoToBotGame;

    private void HandleGoToMainMenu()
    {
        Deactivate();
        soundPresenter.PlayOneShot("ClickEnter");
        OnGoToMainMenu?.Invoke();
    }

    private void HandleGoToBotGame()
    {
        Deactivate();
        soundPresenter.PlayOneShot("ClickEnter");
        OnGoToBotGame?.Invoke();
    }

    #endregion
}
