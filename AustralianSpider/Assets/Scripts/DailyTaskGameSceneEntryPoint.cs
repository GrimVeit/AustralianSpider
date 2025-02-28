using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyTaskGameSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private GameDesignGroup gameDesignGroup;
    [SerializeField] private CoverCardDesignGroup coverCardDesignGroup;
    [SerializeField] private FaceCardDesignGroup faceCardDesignGroup;
    [SerializeField] private GameTypeGroup gameTypeGroup;
    [SerializeField] private UIDailyTaskGameSceneRoot sceneRootPrefab;

    private UIDailyTaskGameSceneRoot sceneRoot;
    private ViewContainer viewContainer;

    private SoundPresenter soundPresenter;
    private ParticleEffectPresenter particleEffectPresenter;
    private BankPresenter bankPresenter;

    private StoreGameDesignPresenter storeGameDesignPresenter;
    private GameDesignPresenter gameDesignPresenter;

    private StoreCoverCardDesignPresenter storeCoverCardDesignPresenter;
    private StoreFaceCardDesignPresenter storeFaceCardDesignPresenter;
    private StoreGameTypePresenter storeGameTypePresenter;
    private StoreDailyTaskPresenter storeDailyTaskPresenter;

    private StoreCardPresenter storeCardPresenter;
    private CardColumnPresenter cardColumnPresenter;
    private CardMotionHistoryPresenter cardMotionHistoryPresenter;

    private TimerPresenter timerPresenter;
    private ScorePresenter scorePresenter;
    private MotionCounterPresenter motionCounterPresenter;
    private MotionHintPresenter motionHintPresenter;

    private DailyTaskGameStateMachine stateMachine;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = sceneRootPrefab;

        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        sceneRoot.Activate();

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        soundPresenter = new SoundPresenter(new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS), viewContainer.GetView<SoundView>());
        soundPresenter.Initialize();

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Initialize();

        particleEffectPresenter = new ParticleEffectPresenter(new ParticleEffectModel(), viewContainer.GetView<ParticleEffectView>());
        particleEffectPresenter.Initialize();

        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());
        bankPresenter.Initialize();

        storeGameDesignPresenter = new StoreGameDesignPresenter(new StoreGameDesignModel(gameDesignGroup));
        gameDesignPresenter = new GameDesignPresenter(new GameDesignModel(), viewContainer.GetView<GameDesignView>());

        storeCoverCardDesignPresenter = new StoreCoverCardDesignPresenter(new StoreCoverCardDesignModel(coverCardDesignGroup));
        storeFaceCardDesignPresenter = new StoreFaceCardDesignPresenter(new StoreFaceCardDesignModel(faceCardDesignGroup));
        storeGameTypePresenter = new StoreGameTypePresenter(new StoreGameTypeModel(gameTypeGroup));

        storeCardPresenter = new StoreCardPresenter(new StoreCardModel(), viewContainer.GetView<StoreCardView>());
        cardColumnPresenter = new CardColumnPresenter(new CardColumnModel(), viewContainer.GetView<CardColumnView>());
        cardMotionHistoryPresenter = new CardMotionHistoryPresenter(new CardMotionHistoryModel(), viewContainer.GetView<CardMotionHistoryView>());

        storeDailyTaskPresenter = new StoreDailyTaskPresenter(new StoreDailyTaskModel());

        timerPresenter = new TimerPresenter(new TimerModel(), viewContainer.GetView<TimerView_MinutesSeconds>());
        scorePresenter = new ScorePresenter(new ScoreModel(bankPresenter, soundPresenter), viewContainer.GetView<ScoreView>());
        motionCounterPresenter = new MotionCounterPresenter(new MotionCounterModel(bankPresenter, soundPresenter), viewContainer.GetView<MotionCounterView>());
        motionHintPresenter = new MotionHintPresenter(new MotionHintModel(), viewContainer.GetView<MotionHintView>());

        stateMachine = new DailyTaskGameStateMachine(
            storeGameDesignPresenter,
            storeCoverCardDesignPresenter,
            storeFaceCardDesignPresenter,
            storeGameTypePresenter,
            storeCardPresenter,
            gameDesignPresenter,
            cardColumnPresenter,
            sceneRoot,
            scorePresenter,
            motionCounterPresenter,
            cardMotionHistoryPresenter,
            timerPresenter,
            motionHintPresenter,
            storeDailyTaskPresenter);

        ActivateEvents();

        timerPresenter.Initialize();
        scorePresenter.Initialize();
        motionCounterPresenter.Initialize();
        motionHintPresenter.Initialize();

        storeCardPresenter.Initialize();
        gameDesignPresenter.Initialize();
        storeGameDesignPresenter.Initialize();

        cardMotionHistoryPresenter.Initialize();
        cardColumnPresenter.Initialize();
        storeCoverCardDesignPresenter.Initialize();
        storeFaceCardDesignPresenter.Initialize();
        storeGameTypePresenter.Initialize();
        storeDailyTaskPresenter.Initalize();

        stateMachine.Initialize();
    }

    private void ActivateEvents()
    {
        ActivateTransitionEvents();

        storeGameDesignPresenter.OnSelectGameDesign += gameDesignPresenter.SetGameDesign;

        storeCoverCardDesignPresenter.OnSelectCoverCardDesign += storeCardPresenter.SetCoverCardDesign;
        storeFaceCardDesignPresenter.OnSelectFaceCardDesign += storeCardPresenter.SetFaceCardDesign;
        storeGameTypePresenter.OnSelectGameType += storeCardPresenter.SetGameType;

        storeCardPresenter.OnDealCards_Value += cardColumnPresenter.DealCards;
    }

    private void DeactivateEvents()
    {
        DeactivateTransitionEvents();

        storeGameDesignPresenter.OnSelectGameDesign -= gameDesignPresenter.SetGameDesign;

        storeCoverCardDesignPresenter.OnSelectCoverCardDesign -= storeCardPresenter.SetCoverCardDesign;
        storeFaceCardDesignPresenter.OnSelectFaceCardDesign -= storeCardPresenter.SetFaceCardDesign;
        storeGameTypePresenter.OnSelectGameType -= storeCardPresenter.SetGameType;

        storeCardPresenter.OnDealCards_Value -= cardColumnPresenter.DealCards;
    }

    private void ActivateTransitionEvents()
    {
        sceneRoot.OnClickToExit += HandleGoToMainMenu;
    }

    private void DeactivateTransitionEvents()
    {
        sceneRoot.OnClickToExit -= HandleGoToMainMenu;
    }

    public void Dispose()
    {
        DeactivateEvents();

        sceneRoot?.Dispose();
        soundPresenter?.Dispose();
        scorePresenter?.Dispose();
        bankPresenter?.Dispose();

        timerPresenter?.Dispose();
        motionCounterPresenter?.Dispose();
        cardMotionHistoryPresenter?.Dispose();
        motionHintPresenter?.Dispose();

        gameDesignPresenter?.Dispose();
        storeGameDesignPresenter?.Dispose();


        cardColumnPresenter?.Dispose();
        storeCoverCardDesignPresenter?.Dispose();
        storeFaceCardDesignPresenter?.Dispose();
        storeGameTypePresenter?.Dispose();
        storeCardPresenter?.Dispose();
        storeDailyTaskPresenter?.Dispose();

        stateMachine?.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region Input

    public event Action OnGoToMainMenu;

    private void HandleGoToMainMenu()
    {
        sceneRoot.Deactivate();
        soundPresenter.Dispose();
        OnGoToMainMenu?.Invoke();
    }

    #endregion
}
