using System;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private BuyDesignPrices buyDesignPrices;
    [SerializeField] private FaceCardDesignGroup faceCardDesignGroup;
    [SerializeField] private CoverCardDesignGroup coverCardDesignGroup;
    [SerializeField] private GameDesignGroup gameDesignGroup;
    [SerializeField] private GameTypeGroup gameTypeGroup;
    [SerializeField] private UIMainMenuRoot menuRootPrefab;

    private UIMainMenuRoot sceneRoot;
    private ViewContainer viewContainer;

    private BankPresenter bankPresenter;
    private ParticleEffectPresenter particleEffectPresenter;
    private SoundPresenter soundPresenter;

    private StoreFaceCardDesignPresenter storeFaceCardDesignPresenter;
    private SelectFaceCardDesignPresenter selectFaceCardDesignPresenter;
    private FaceCardDesignVisualizePresenter faceCardDesignVisualizePresenter;
    private BuyFaceCardDesignPresenter buyFaceCardDesignPresenter;

    private StoreCoverCardDesignPresenter storeCoverCardDesignPresenter;
    private SelectCoverCardDesignPresenter selectCoverCardDesignPresenter;
    private CoverCardDesignVisualizePresenter coverCardDesignVisualizePresenter;
    private BuyCoverCardDesignPresenter buyCoverCardDesignPresenter;

    private StoreGameDesignPresenter storeGameDesignPresenter;
    private SelectGameDesignPresenter selectGameDesignPresenter;
    private GameDesignVisualizePresenter gameDesignVisualizePresenter;
    private BuyGameDesignPresenter buyGameDesignPresenter;

    private StoreGameTypePresenter storeGameTypePresenter;
    private SelectGameTypePresenter selectGameTypePresenter;

    private StoreDailyTaskPresenter storeDailyTaskPresenter;
    private SelectDailyTaskPresenter selectDailyTaskPresenter;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = menuRootPrefab;
 
        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        soundPresenter = new SoundPresenter
            (new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS),
            viewContainer.GetView<SoundView>());

        particleEffectPresenter = new ParticleEffectPresenter
            (new ParticleEffectModel(),
            viewContainer.GetView<ParticleEffectView>());

        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());

        storeFaceCardDesignPresenter = new StoreFaceCardDesignPresenter(new StoreFaceCardDesignModel(faceCardDesignGroup));
        selectFaceCardDesignPresenter = new SelectFaceCardDesignPresenter(new SelectFaceCardDesignModel(), viewContainer.GetView<SelectFaceCardDesignView>());
        faceCardDesignVisualizePresenter = new FaceCardDesignVisualizePresenter(new FaceCardDesignVisualizeModel(), viewContainer.GetView<FaceCardDesignVisualizeView>());
        buyFaceCardDesignPresenter = new BuyFaceCardDesignPresenter(new BuyFaceCardDesignModel(PlayerPrefsKeys.BUY_LEVEL_FACE_CARD, buyDesignPrices, bankPresenter), viewContainer.GetView<BuyFaceCardDesignView>());

        storeCoverCardDesignPresenter = new StoreCoverCardDesignPresenter(new StoreCoverCardDesignModel(coverCardDesignGroup));
        selectCoverCardDesignPresenter = new SelectCoverCardDesignPresenter(new SelectCoverCardDesignModel(), viewContainer.GetView<SelectCoverCardDesignView>());
        coverCardDesignVisualizePresenter = new CoverCardDesignVisualizePresenter(new CoverCardDesignVisualizeModel(), viewContainer.GetView<CoverCardDesignVisualizeView>());
        buyCoverCardDesignPresenter = new BuyCoverCardDesignPresenter(new BuyCoverCardDesignModel(PlayerPrefsKeys.BUY_LEVEL_COVER_CARD, buyDesignPrices, bankPresenter), viewContainer.GetView<BuyCoverCardDesignView>());

        storeGameDesignPresenter = new StoreGameDesignPresenter(new StoreGameDesignModel(gameDesignGroup));
        selectGameDesignPresenter = new SelectGameDesignPresenter(new SelectGameDesignModel(), viewContainer.GetView<SelectGameDesignView>());
        gameDesignVisualizePresenter = new GameDesignVisualizePresenter(new GameDesignVisualizeModel(), viewContainer.GetView<GameDesignVisualizeView>());
        buyGameDesignPresenter = new BuyGameDesignPresenter(new BuyGameDesignModel(PlayerPrefsKeys.BUY_LEVEL_BACKGROUND, buyDesignPrices, bankPresenter), viewContainer.GetView<BuyGameDesignView>());

        storeGameTypePresenter = new StoreGameTypePresenter(new StoreGameTypeModel(gameTypeGroup));
        selectGameTypePresenter = new SelectGameTypePresenter(new SelectGameTypeModel(), viewContainer.GetView<SelectGameTypeView>());

        storeDailyTaskPresenter = new StoreDailyTaskPresenter(new StoreDailyTaskModel());
        selectDailyTaskPresenter = new SelectDailyTaskPresenter(new SelectDailyTaskModel(), viewContainer.GetView<SelectDailyTaskView>());

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Activate();

        ActivateEvents();

        soundPresenter.Initialize();
        particleEffectPresenter.Initialize();
        sceneRoot.Initialize();
        bankPresenter.Initialize();

        selectDailyTaskPresenter.Initialize();

        selectGameTypePresenter.Initialize();

        buyGameDesignPresenter.Initialize();
        gameDesignVisualizePresenter.Initialize();
        selectGameDesignPresenter.Initialize();

        buyCoverCardDesignPresenter.Initialize();
        coverCardDesignVisualizePresenter.Initialize();
        selectCoverCardDesignPresenter.Initialize();

        buyFaceCardDesignPresenter.Initialize();
        faceCardDesignVisualizePresenter.Initialize();
        selectFaceCardDesignPresenter.Initialize();

        storeDailyTaskPresenter.Initalize();
        storeGameTypePresenter.Initialize();
        storeGameDesignPresenter.Initialize();
        storeCoverCardDesignPresenter.Initialize();
        storeFaceCardDesignPresenter.Initialize();
    }

    private void ActivateEvents()
    {
        ActivateTransitionsSceneEvents();

        Debug.Log("KKK");

        storeFaceCardDesignPresenter.OnOpenFaceCardDesign += selectFaceCardDesignPresenter.SetOpenFaceCardDesign;
        storeFaceCardDesignPresenter.OnCloseFaceCardDesign += selectFaceCardDesignPresenter.SetCloseFaceCardDesign;
        selectFaceCardDesignPresenter.OnChooseFaceCardDesign += storeFaceCardDesignPresenter.SelectFaceCardDesign;
        storeFaceCardDesignPresenter.OnSelectFaceCardDesign += selectFaceCardDesignPresenter.SelectFaceCardDesign;
        storeFaceCardDesignPresenter.OnDeselectFaceCardDesign += selectFaceCardDesignPresenter.DeselectFaceCardDesign;
        storeFaceCardDesignPresenter.OnSelectFaceCardDesign += faceCardDesignVisualizePresenter.SetFaceCardDesign;

        storeFaceCardDesignPresenter.OnOpenFaceCardDesign += buyFaceCardDesignPresenter.SetOpenCoverCardDesign;
        storeFaceCardDesignPresenter.OnCloseFaceCardDesign += buyFaceCardDesignPresenter.SetCloseCoverCardDesign;
        buyFaceCardDesignPresenter.OnBuyFaceCardDesign += storeFaceCardDesignPresenter.BuyFaceCardDesign;





        storeCoverCardDesignPresenter.OnOpenCoverCardDesign += selectCoverCardDesignPresenter.SetOpenCoverCardDesign;
        storeCoverCardDesignPresenter.OnCloseCoverCardDesign += selectCoverCardDesignPresenter.SetCloseCoverCardDesign;
        selectCoverCardDesignPresenter.OnChooseCoverCardDesign += storeCoverCardDesignPresenter.SelectCoverCardDesign;
        storeCoverCardDesignPresenter.OnSelectCoverCardDesign += selectCoverCardDesignPresenter.SelectCoverCardDesign;
        storeCoverCardDesignPresenter.OnDeselectCoverCardDesign += selectCoverCardDesignPresenter.DeselectCoverCardDesign;
        storeCoverCardDesignPresenter.OnSelectCoverCardDesign += coverCardDesignVisualizePresenter.SetCoverCardDesign;

        storeCoverCardDesignPresenter.OnOpenCoverCardDesign += buyCoverCardDesignPresenter.SetOpenCoverCardDesign;
        storeCoverCardDesignPresenter.OnCloseCoverCardDesign += buyCoverCardDesignPresenter.SetCloseCoverCardDesign;
        buyCoverCardDesignPresenter.OnBuyCoverCardDesign += storeCoverCardDesignPresenter.BuyCoverCardDesign;





        storeGameDesignPresenter.OnOpenGameDesign += selectGameDesignPresenter.SetOpenGameDesign;
        storeGameDesignPresenter.OnCloseGameDesign += selectGameDesignPresenter.SetCloseGameDesign;
        selectGameDesignPresenter.OnChooseGameDesign += storeGameDesignPresenter.SelectGameDesign;
        storeGameDesignPresenter.OnSelectGameDesign += selectGameDesignPresenter.SelectGameDesign;
        storeGameDesignPresenter.OnDeselectGameDesign += selectGameDesignPresenter.DeselectGameDesign;
        storeGameDesignPresenter.OnSelectGameDesign += gameDesignVisualizePresenter.SetGameDesign;

        storeGameDesignPresenter.OnOpenGameDesign += buyGameDesignPresenter.SetOpenGameDesign;
        storeGameDesignPresenter.OnCloseGameDesign += buyGameDesignPresenter.SetCloseGameDesign;
        buyGameDesignPresenter.OnBuyFaceCardDesign += storeGameDesignPresenter.BuyGameDesign;





        selectGameTypePresenter.OnChooseGameType += storeGameTypePresenter.SelectGameType;
        storeGameTypePresenter.OnSelectGameType += selectGameTypePresenter.SelectGameType;
        storeGameTypePresenter.OnDeselectGameType += selectGameTypePresenter.DeselectGameType;




        selectDailyTaskPresenter.OnChooseDailyTask += storeDailyTaskPresenter.SelectDailyTask;
        storeDailyTaskPresenter.OnGetDayOfWeekFirstDayMonth += selectDailyTaskPresenter.SetDayOfWeakFirstDayMonth;
        storeDailyTaskPresenter.OnChangeStatusDailyTask += selectDailyTaskPresenter.SetDailyTaskData;
        storeDailyTaskPresenter.OnSelectDailyTask += selectDailyTaskPresenter.SelectDailyTask;
        storeDailyTaskPresenter.OnDeselectDailyTask += selectDailyTaskPresenter.DeselectDailyTask;
    }

    private void DeactivateEvents()
    {
        DeactivateTransitionsSceneEvents();

        storeFaceCardDesignPresenter.OnOpenFaceCardDesign -= selectFaceCardDesignPresenter.SetOpenFaceCardDesign;
        storeFaceCardDesignPresenter.OnCloseFaceCardDesign -= selectFaceCardDesignPresenter.SetCloseFaceCardDesign;
        selectFaceCardDesignPresenter.OnChooseFaceCardDesign -= storeFaceCardDesignPresenter.SelectFaceCardDesign;
        storeFaceCardDesignPresenter.OnSelectFaceCardDesign -= selectFaceCardDesignPresenter.SelectFaceCardDesign;
        storeFaceCardDesignPresenter.OnDeselectFaceCardDesign -= selectFaceCardDesignPresenter.DeselectFaceCardDesign;
        storeFaceCardDesignPresenter.OnSelectFaceCardDesign -= faceCardDesignVisualizePresenter.SetFaceCardDesign;

        storeFaceCardDesignPresenter.OnOpenFaceCardDesign -= buyFaceCardDesignPresenter.SetOpenCoverCardDesign;
        storeFaceCardDesignPresenter.OnCloseFaceCardDesign -= buyFaceCardDesignPresenter.SetCloseCoverCardDesign;
        buyFaceCardDesignPresenter.OnBuyFaceCardDesign -= storeFaceCardDesignPresenter.BuyFaceCardDesign;


        storeCoverCardDesignPresenter.OnOpenCoverCardDesign -= selectCoverCardDesignPresenter.SetOpenCoverCardDesign;
        storeCoverCardDesignPresenter.OnCloseCoverCardDesign -= selectCoverCardDesignPresenter.SetCloseCoverCardDesign;
        selectCoverCardDesignPresenter.OnChooseCoverCardDesign -= storeCoverCardDesignPresenter.SelectCoverCardDesign;
        storeCoverCardDesignPresenter.OnSelectCoverCardDesign -= selectCoverCardDesignPresenter.SelectCoverCardDesign;
        storeCoverCardDesignPresenter.OnDeselectCoverCardDesign -= selectCoverCardDesignPresenter.DeselectCoverCardDesign;
        storeCoverCardDesignPresenter.OnSelectCoverCardDesign -= coverCardDesignVisualizePresenter.SetCoverCardDesign;

        storeCoverCardDesignPresenter.OnOpenCoverCardDesign -= buyCoverCardDesignPresenter.SetOpenCoverCardDesign;
        storeCoverCardDesignPresenter.OnCloseCoverCardDesign -= buyCoverCardDesignPresenter.SetCloseCoverCardDesign;
        buyCoverCardDesignPresenter.OnBuyCoverCardDesign -= storeCoverCardDesignPresenter.BuyCoverCardDesign;


        storeGameDesignPresenter.OnOpenGameDesign -= selectGameDesignPresenter.SetOpenGameDesign;
        storeGameDesignPresenter.OnCloseGameDesign -= selectGameDesignPresenter.SetCloseGameDesign;
        selectGameDesignPresenter.OnChooseGameDesign -= storeGameDesignPresenter.SelectGameDesign;
        storeGameDesignPresenter.OnSelectGameDesign -= selectGameDesignPresenter.SelectGameDesign;
        storeGameDesignPresenter.OnDeselectGameDesign -= selectGameDesignPresenter.DeselectGameDesign;
        storeGameDesignPresenter.OnSelectGameDesign -= gameDesignVisualizePresenter.SetGameDesign;

        storeGameDesignPresenter.OnOpenGameDesign -= buyGameDesignPresenter.SetOpenGameDesign;
        storeGameDesignPresenter.OnCloseGameDesign -= buyGameDesignPresenter.SetCloseGameDesign;
        buyGameDesignPresenter.OnBuyFaceCardDesign -= storeGameDesignPresenter.BuyGameDesign;





        selectGameTypePresenter.OnChooseGameType -= storeGameTypePresenter.SelectGameType;
        storeGameTypePresenter.OnSelectGameType -= selectGameTypePresenter.SelectGameType;
        storeGameTypePresenter.OnDeselectGameType -= selectGameTypePresenter.DeselectGameType;


        selectDailyTaskPresenter.OnChooseDailyTask -= storeDailyTaskPresenter.SelectDailyTask;
        storeDailyTaskPresenter.OnGetDayOfWeekFirstDayMonth -= selectDailyTaskPresenter.SetDayOfWeakFirstDayMonth;
        storeDailyTaskPresenter.OnChangeStatusDailyTask -= selectDailyTaskPresenter.SetDailyTaskData;
        storeDailyTaskPresenter.OnSelectDailyTask -= selectDailyTaskPresenter.SelectDailyTask;
        storeDailyTaskPresenter.OnDeselectDailyTask -= selectDailyTaskPresenter.DeselectDailyTask;
    }

    private void ActivateTransitionsSceneEvents()
    {
        sceneRoot.OnPlay += HandleGoToGame;
    }

    private void DeactivateTransitionsSceneEvents()
    {
        sceneRoot.OnPlay -= HandleGoToGame;
    }

    private void Deactivate()
    {
        sceneRoot.Deactivate();
        soundPresenter?.Dispose();
    }

    private void Dispose()
    {
        DeactivateEvents();

        soundPresenter?.Dispose();
        sceneRoot?.Dispose();
        particleEffectPresenter?.Dispose();
        bankPresenter?.Dispose();


        selectDailyTaskPresenter?.Dispose();

        selectGameTypePresenter?.Dispose();

        buyGameDesignPresenter?.Dispose();
        gameDesignVisualizePresenter?.Dispose();
        selectGameDesignPresenter?.Dispose();

        buyCoverCardDesignPresenter?.Dispose();
        coverCardDesignVisualizePresenter?.Dispose();
        selectCoverCardDesignPresenter?.Dispose();

        buyFaceCardDesignPresenter?.Dispose();
        faceCardDesignVisualizePresenter?.Dispose();
        selectFaceCardDesignPresenter?.Dispose();


        storeDailyTaskPresenter?.Dispose();
        storeGameTypePresenter?.Dispose();
        storeGameDesignPresenter?.Dispose();
        storeCoverCardDesignPresenter?.Dispose();
        storeFaceCardDesignPresenter.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region Input actions

    public event Action OnGoToGame;

    private void HandleGoToGame()
    {
        Deactivate();
        OnGoToGame?.Invoke();
    }

    #endregion
}
