using System;
using UnityEngine;

public class GameSoloSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private UIGameSoloRoot menuRootPrefab;

    private UIGameSoloRoot sceneRoot;
    private ViewContainer viewContainer;

    private ParticleEffectPresenter particleEffectPresenter;
    private SoundPresenter soundPresenter;
    private BankPresenter bankPresenter;

    private DiceRollPresenter diceRollPresenter;
    private YatzyCombinationPresenter yatzyCombinationPresenter;
    private PlayerPresenter playerPresenter;

    private YatzyEffectPresenter yatzyEffectPresenter;
    private DiceEffectPresenter diceEffectPresenter;

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

        diceRollPresenter = new DiceRollPresenter
            (new DiceRollModel(5, 3, soundPresenter), 
            viewContainer.GetView<DiceRollView>());
        diceRollPresenter.Initialize();

        yatzyCombinationPresenter = new YatzyCombinationPresenter
            (new YatzyCombinationModel(13, soundPresenter), 
            viewContainer.GetView<YatzyCombinationView>());
        yatzyCombinationPresenter.Initialize();

        playerPresenter = new PlayerPresenter
            (new PlayerModel(PlayerPrefsKeys.NICKNAME, PlayerPrefsKeys.IMAGE_INDEX), 
            viewContainer.GetView<PlayerView>());
        playerPresenter.Initialize();

        yatzyEffectPresenter = new YatzyEffectPresenter(new YatzyEffectModel_First(particleEffectPresenter));
        diceEffectPresenter = new DiceEffectPresenter(new DiceEffectModel_First(particleEffectPresenter));

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Initialize();

        ActivateEvents();

        sceneRoot.Activate();
    }

    private void ActivateEvents()
    {
        ActivateTransitionsSceneEvents();

        diceRollPresenter.OnGetAllDiceValues += yatzyCombinationPresenter.SetNumbersCombination;

        diceRollPresenter.OnStartRoll += yatzyCombinationPresenter.Deactivate;
        diceRollPresenter.OnStopRoll += yatzyCombinationPresenter.Activate;
        diceRollPresenter.OnStartRoll += diceRollPresenter.DeactivateFreezeToggle;
        diceRollPresenter.OnStopRoll += diceRollPresenter.ActivateFreezeToggle;

        yatzyCombinationPresenter.OnFreezeYatzyCombination += diceRollPresenter.Reload;
        yatzyCombinationPresenter.OnFreezeYatzyCombination += yatzyCombinationPresenter.Deactivate;
        yatzyCombinationPresenter.OnFreezeYatzyCombination += diceRollPresenter.DeactivateFreezeToggle;

        yatzyCombinationPresenter.OnSelectCombination_Index += yatzyEffectPresenter.SetYatzyCombinationIndex;
        diceRollPresenter.OnFreezeDice_Index += diceEffectPresenter.SetDiceIndex;
    }

    private void DeactivateEvents()
    {
        DeactivateTransitionsSceneEvents();

        diceRollPresenter.OnGetAllDiceValues -= yatzyCombinationPresenter.SetNumbersCombination;

        diceRollPresenter.OnStartRoll -= yatzyCombinationPresenter.Deactivate;
        diceRollPresenter.OnStopRoll -= yatzyCombinationPresenter.Activate;
        diceRollPresenter.OnStartRoll -= diceRollPresenter.DeactivateFreezeToggle;
        diceRollPresenter.OnStopRoll -= diceRollPresenter.ActivateFreezeToggle;

        yatzyCombinationPresenter.OnFreezeYatzyCombination -= diceRollPresenter.Reload;
        yatzyCombinationPresenter.OnFreezeYatzyCombination -= diceRollPresenter.DeactivateFreezeToggle;

        yatzyCombinationPresenter.OnSelectCombination_Index -= yatzyEffectPresenter.SetYatzyCombinationIndex;
        diceRollPresenter.OnFreezeDice_Index -= diceEffectPresenter.SetDiceIndex;
    }

    private void ActivateTransitionsSceneEvents()
    {
        sceneRoot.OnClickToGoMainMenuFromMainPanel += HandleGoToMainMenu;
        sceneRoot.OnClickToGoMainMenuFromFinishPanel += HandleGoToMainMenu;
        sceneRoot.OnClickToGoSoloGameFromFinishPanel += HandleGoToSoloGame;
        yatzyCombinationPresenter.OnFinishGame += sceneRoot.OpenFinishPanel;

        diceRollPresenter.OnGetFullAttempt += sceneRoot.OpenRollPanel;
        diceRollPresenter.OnLoseFirstAttempt += sceneRoot.OpenPlayRollPanel;
    }

    private void DeactivateTransitionsSceneEvents()
    {
        sceneRoot.OnClickToGoMainMenuFromMainPanel -= HandleGoToMainMenu;
        sceneRoot.OnClickToGoMainMenuFromFinishPanel -= HandleGoToMainMenu;
        sceneRoot.OnClickToGoSoloGameFromFinishPanel -= HandleGoToSoloGame;
        yatzyCombinationPresenter.OnFinishGame -= sceneRoot.OpenFinishPanel;

        diceRollPresenter.OnGetFullAttempt -= sceneRoot.OpenRollPanel;
        diceRollPresenter.OnLoseFirstAttempt -= sceneRoot.OpenPlayRollPanel;
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

        diceRollPresenter?.Dispose();
        yatzyCombinationPresenter?.Dispose();
        playerPresenter?.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region Input actions

    public event Action OnGoToMainMenu;
    public event Action OnGoToSoloGame;

    private void HandleGoToMainMenu()
    {
        Deactivate();
        soundPresenter.PlayOneShot("ClickEnter");
        OnGoToMainMenu?.Invoke();
    }

    private void HandleGoToSoloGame()
    {
        Deactivate();
        soundPresenter.PlayOneShot("ClickEnter");
        OnGoToSoloGame?.Invoke();
    }

    #endregion
}
