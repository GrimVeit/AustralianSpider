using System;
using UnityEngine;

public class RouletteEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private UIRouletteRoot sceneRootPrefab;

    private UIRouletteRoot sceneRoot;
    private ViewContainer viewContainer;

    private ParticleEffectPresenter particleEffectPresenter;
    private SoundPresenter soundPresenter;
    private BankPresenter bankPresenter;

    private PseudoChipPresenter pseudoChipPresenter;
    private ChipPresenter chipPresenter;

    private RouletteBallPresenter rouletteBallPresenter;
    private RoulettePresenter roulettePresenter;
    private RouletteBetPresenter rouletteBetPresenter;
    private RouletteResultPresenter rouletteResultPresenter;
    private RouletteHistoryPresenter rouletteHistoryPresenter;

    private RouletteDesignPresenter rouletteDesignPresenter;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = Instantiate(sceneRootPrefab);

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

        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());
        bankPresenter.Initialize();

        pseudoChipPresenter = new PseudoChipPresenter(new PseudoChipModel(bankPresenter, soundPresenter), viewContainer.GetView<PseudoChipView>());
        pseudoChipPresenter.Initialize();

        chipPresenter = new ChipPresenter(new ChipModel(soundPresenter), viewContainer.GetView<ChipView>());
        chipPresenter.Initialize();

        rouletteBallPresenter = new RouletteBallPresenter(new RouletteBallModel(soundPresenter), viewContainer.GetView<RouletteBallView>());
        rouletteBallPresenter.Initialize();

        roulettePresenter = new RoulettePresenter(new RouletteModel(soundPresenter), viewContainer.GetView<RouletteView>());
        roulettePresenter.Initialize();

        rouletteBetPresenter = new RouletteBetPresenter(new RouletteBetModel(bankPresenter), viewContainer.GetView<RouletteBetView>());
        rouletteBetPresenter.Initialize();

        rouletteResultPresenter = new RouletteResultPresenter(new RouletteResultModel(), viewContainer.GetView<RouletteResultView>());
        rouletteResultPresenter.Initialize();

        rouletteHistoryPresenter = new RouletteHistoryPresenter(new RouletteHistoryModel(soundPresenter), viewContainer.GetView<RouletteHistoryView>());
        rouletteHistoryPresenter.Initialize();

        rouletteDesignPresenter = new RouletteDesignPresenter(new RouletteDesignModel(), viewContainer.GetView<RouletteDesignView>());
        rouletteDesignPresenter.Initialize();

        ActivateTransferEvents();
        ActivateEvents();

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.SetParticleEffectProvider(particleEffectPresenter);
        sceneRoot.Initialize();
        sceneRoot.Activate();
    }

    private void ActivateEvents()
    {
        rouletteBetPresenter.OnNoneRetractedChip += chipPresenter.NoneRetractChip;
        rouletteBetPresenter.OnDestroyedChip += chipPresenter.FallChip;

        pseudoChipPresenter.OnSpawnChip += chipPresenter.SpawnChip;
        rouletteBallPresenter.OnBallStopped += roulettePresenter.RollBallToSlot;
        roulettePresenter.OnGetRouletteSlotValue += rouletteBetPresenter.GetRouletteSlotValue;
        roulettePresenter.OnGetRouletteSlotValue += rouletteResultPresenter.ShowResult;

        rouletteResultPresenter.OnStartShowResult += rouletteBetPresenter.SearchWin;
        rouletteResultPresenter.OnFinishShowResult += rouletteBetPresenter.ShowResult;
        rouletteResultPresenter.OnStartHideResult += rouletteHistoryPresenter.AddRouletteNumber;

        rouletteBetPresenter.OnStartHideResult += rouletteResultPresenter.HideResult;
        rouletteBetPresenter.OnFinishHideResult += sceneRoot.CloseSpinPanel;
        rouletteBetPresenter.OnFinishHideResult += rouletteBetPresenter.ReturnChips;
    }

    private void ActivateTransferEvents()
    {
        sceneRoot.OnClickToBackButton += HandleGoToMainMenu;
        sceneRoot.OnClickToSpinButton += sceneRoot.OpenSpinPanel;
    }

    private void DeactivateEvents()
    {
        pseudoChipPresenter.OnSpawnChip -= chipPresenter.SpawnChip;
        rouletteBallPresenter.OnBallStopped -= roulettePresenter.RollBallToSlot;
        roulettePresenter.OnGetRouletteSlotValue -= rouletteResultPresenter.ShowResult;
        roulettePresenter.OnGetRouletteSlotValue -= rouletteBetPresenter.GetRouletteSlotValue;

        rouletteResultPresenter.OnFinishShowResult -= rouletteBetPresenter.SearchWin;
        rouletteResultPresenter.OnFinishShowResult -= rouletteBetPresenter.ShowResult;
        rouletteResultPresenter.OnStartHideResult -= rouletteHistoryPresenter.AddRouletteNumber;

        rouletteBetPresenter.OnStartHideResult -= rouletteResultPresenter.HideResult;
        rouletteBetPresenter.OnFinishHideResult -= sceneRoot.CloseSpinPanel;

        rouletteBetPresenter.OnFinishHideResult -= rouletteBetPresenter.ReturnChips;
        rouletteBetPresenter.OnNoneRetractedChip -= chipPresenter.NoneRetractChip;
        rouletteBetPresenter.OnDestroyedChip -= chipPresenter.FallChip;
    }

    private void DeactivateTransferEvents()
    {
        sceneRoot.OnClickToBackButton -= HandleGoToMainMenu;
        sceneRoot.OnClickToSpinButton -= sceneRoot.OpenSpinPanel;
    }

    private void Dispose()
    {
        DeactivateEvents();
        DeactivateTransferEvents();
        sceneRoot?.Deactivate();

        sceneRoot?.Dispose();
        particleEffectPresenter?.Dispose();
        soundPresenter?.Dispose();
        bankPresenter?.Dispose();

        pseudoChipPresenter?.Dispose();
        chipPresenter?.Dispose();
        rouletteBallPresenter?.Dispose();
        roulettePresenter?.Dispose();
        rouletteBetPresenter?.Dispose();
        rouletteResultPresenter?.Dispose();
        rouletteHistoryPresenter?.Dispose();

        rouletteDesignPresenter?.Dispose();
    }

    #region Input actions

    public event Action GoToMainMenu_Action;


    private void HandleGoToMainMenu()
    {
        Dispose();
        GoToMainMenu_Action?.Invoke();
    }

    #endregion
}
