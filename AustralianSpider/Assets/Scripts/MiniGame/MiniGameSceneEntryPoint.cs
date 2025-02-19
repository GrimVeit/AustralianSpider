using System;
using UnityEngine;

public class MiniGameSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private GameDesignGroup gameDesignGroup;
    [SerializeField] private CoverCardDesignGroup coverCardDesignGroup;
    [SerializeField] private FaceCardDesignGroup faceCardDesignGroup;
    [SerializeField] private GameTypeGroup gameTypeGroup;
    [SerializeField] private UIMiniGameSceneRoot sceneRootPrefab;

    private UIMiniGameSceneRoot sceneRoot;
    private ViewContainer viewContainer;

    private SoundPresenter soundPresenter;
    private ParticleEffectPresenter particleEffectPresenter;
    private BankPresenter bankPresenter;

    private StoreGameDesignPresenter storeGameDesignPresenter;
    private GameDesignPresenter gameDesignPresenter;

    private StoreCoverCardDesignPresenter storeCoverCardDesignPresenter;
    private StoreFaceCardDesignPresenter storeFaceCardDesignPresenter;
    private StoreGameTypePresenter storeGameTypePresenter;

    private StoreCardPresenter storeCardPresenter;
    private CardColumnPresenter cardColumnPresenter;

    private TimerPresenter timerPresenter;
    private ScorePresenter scorePresenter;
    private MotionCounterPresenter motionCounterPresenter;

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

        timerPresenter = new TimerPresenter(new TimerModel(), viewContainer.GetView<TimerView_MinutesSeconds>());
        scorePresenter = new ScorePresenter(new ScoreModel(bankPresenter, soundPresenter), viewContainer.GetView<ScoreView>());
        motionCounterPresenter = new MotionCounterPresenter(new MotionCounterModel(bankPresenter, soundPresenter), viewContainer.GetView<MotionCounterView>());

        ActivateEvents();

        timerPresenter.Initialize();
        scorePresenter.Initialize();
        motionCounterPresenter.Initialize();

        gameDesignPresenter.Initialize();
        storeGameDesignPresenter.Initialize();

        cardColumnPresenter.Initialize();
        storeCoverCardDesignPresenter.Initialize();
        storeFaceCardDesignPresenter.Initialize();
        storeGameTypePresenter.Initialize();
        storeCardPresenter.Initialize();

        storeCardPresenter.CreateCards();
        storeCardPresenter.DealCards();
        timerPresenter.ActivateTimer();
    }

    private void ActivateEvents()
    {
        ActivateTransitionEvents();

        storeGameDesignPresenter.OnSelectGameDesign += gameDesignPresenter.SetGameDesign;

        storeCoverCardDesignPresenter.OnSelectCoverCardDesign += storeCardPresenter.SetCoverCardDesign;
        storeFaceCardDesignPresenter.OnSelectFaceCardDesign += storeCardPresenter.SetFaceCardDesign;
        storeGameTypePresenter.OnSelectGameType += storeCardPresenter.SetGameType;

        storeCardPresenter.OnDealCards += cardColumnPresenter.DealCards;
        storeCardPresenter.OnDealCardsFromStock += cardColumnPresenter.DealCardsFromStock;

        cardColumnPresenter.OnCardDrop += scorePresenter.RemoveScoreByCardDrop;
        cardColumnPresenter.OnFullCompleteCardGroup += scorePresenter.AddScoreByFullComplect;
        cardColumnPresenter.OnCardDrop += motionCounterPresenter.AddMotion;
    }

    private void DeactivateEvents()
    {
        DeactivateTransitionEvents();

        storeGameDesignPresenter.OnSelectGameDesign -= gameDesignPresenter.SetGameDesign;

        storeCoverCardDesignPresenter.OnSelectCoverCardDesign -= storeCardPresenter.SetCoverCardDesign;
        storeFaceCardDesignPresenter.OnSelectFaceCardDesign -= storeCardPresenter.SetFaceCardDesign;
        storeGameTypePresenter.OnSelectGameType -= storeCardPresenter.SetGameType;

        storeCardPresenter.OnDealCards -= cardColumnPresenter.DealCards;
        storeCardPresenter.OnDealCardsFromStock -= cardColumnPresenter.DealCardsFromStock;

        cardColumnPresenter.OnCardDrop -= scorePresenter.RemoveScoreByCardDrop;
        cardColumnPresenter.OnFullCompleteCardGroup -= scorePresenter.AddScoreByFullComplect;
        cardColumnPresenter.OnCardDrop -= motionCounterPresenter.AddMotion;
    }

    private void ActivateTransitionEvents()
    {
        sceneRoot.OnClickToExit += HandleGoToMainMenu;
        sceneRoot.OnClickToRestart += HandleGoToGame;
    }

    private void DeactivateTransitionEvents()
    {
        sceneRoot.OnClickToExit -= HandleGoToMainMenu;
        sceneRoot.OnClickToRestart -= HandleGoToGame;
    }

    public void Dispose()
    {
        DeactivateEvents();

        sceneRoot?.Dispose();
        soundPresenter?.Dispose();
        bankPresenter?.Dispose();

        timerPresenter?.Dispose();
        scorePresenter?.Dispose();
        motionCounterPresenter?.Dispose();

        gameDesignPresenter?.Dispose();
        storeGameDesignPresenter?.Dispose();


        cardColumnPresenter?.Dispose();
        storeCoverCardDesignPresenter?.Dispose();
        storeFaceCardDesignPresenter?.Dispose();
        storeGameTypePresenter?.Dispose();
        storeCardPresenter?.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region Input

    public event Action OnGoToMainMenu;
    public event Action OnGoToGame;

    private void HandleGoToMainMenu()
    {
        sceneRoot.Deactivate();
        soundPresenter.Dispose();
        OnGoToMainMenu?.Invoke();
    }

    private void HandleGoToGame()
    {
        sceneRoot.Deactivate();
        soundPresenter.Dispose();
        OnGoToGame?.Invoke();
    }

    #endregion
}
