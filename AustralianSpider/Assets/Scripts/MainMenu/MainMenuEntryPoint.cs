using System;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private FaceCardDesignGroup faceCardDesignGroup;
    [SerializeField] private CoverCardDesignGroup coverCardDesignGroup;
    [SerializeField] private GameDesignGroup gameDesignGroup;
    [SerializeField] private UIMainMenuRoot menuRootPrefab;

    private UIMainMenuRoot sceneRoot;
    private ViewContainer viewContainer;

    private BankPresenter bankPresenter;
    private ParticleEffectPresenter particleEffectPresenter;
    private SoundPresenter soundPresenter;

    private StoreFaceCardDesignPresenter storeFaceCardDesignPresenter;
    private SelectFaceCardDesignPresenter selectFaceCardDesignPresenter;
    private FaceCardDesignVisualizePresenter faceCardDesignVisualizePresenter;

    private StoreCoverCardDesignPresenter storeCoverCardDesignPresenter;
    private SelectCoverCardDesignPresenter selectCoverCardDesignPresenter;
    private CoverCardDesignVisualizePresenter coverCardDesignVisualizePresenter;

    private StoreGameDesignPresenter storeGameDesignPresenter;
    private SelectGameDesignPresenter selectGameDesignPresenter;
    private GameDesignVisualizePresenter gameDesignVisualizePresenter;

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

        storeCoverCardDesignPresenter = new StoreCoverCardDesignPresenter(new StoreCoverCardDesignModel(coverCardDesignGroup));
        selectCoverCardDesignPresenter = new SelectCoverCardDesignPresenter(new SelectCoverCardDesignModel(), viewContainer.GetView<SelectCoverCardDesignView>());
        coverCardDesignVisualizePresenter = new CoverCardDesignVisualizePresenter(new CoverCardDesignVisualizeModel(), viewContainer.GetView<CoverCardDesignVisualizeView>());

        storeGameDesignPresenter = new StoreGameDesignPresenter(new StoreGameDesignModel(gameDesignGroup));
        selectGameDesignPresenter = new SelectGameDesignPresenter(new SelectGameDesignModel(), viewContainer.GetView<SelectGameDesignView>());
        gameDesignVisualizePresenter = new GameDesignVisualizePresenter(new GameDesignVisualizeModel(), viewContainer.GetView<GameDesignVisualizeView>());

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Activate();

        ActivateEvents();

        soundPresenter.Initialize();
        particleEffectPresenter.Initialize();
        sceneRoot.Initialize();
        bankPresenter.Initialize();

        gameDesignVisualizePresenter.Initialize();
        selectGameDesignPresenter.Initialize();
        coverCardDesignVisualizePresenter.Initialize();
        selectCoverCardDesignPresenter.Initialize();
        faceCardDesignVisualizePresenter.Initialize();
        selectFaceCardDesignPresenter.Initialize();

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

        storeCoverCardDesignPresenter.OnOpenCoverCardDesign += selectCoverCardDesignPresenter.SetOpenCoverCardDesign;
        storeCoverCardDesignPresenter.OnCloseCoverCardDesign += selectCoverCardDesignPresenter.SetCloseCoverCardDesign;
        selectCoverCardDesignPresenter.OnChooseCoverCardDesign += storeCoverCardDesignPresenter.SelectCoverCardDesign;
        storeCoverCardDesignPresenter.OnSelectCoverCardDesign += selectCoverCardDesignPresenter.SelectCoverCardDesign;
        storeCoverCardDesignPresenter.OnDeselectCoverCardDesign += selectCoverCardDesignPresenter.DeselectCoverCardDesign;
        storeCoverCardDesignPresenter.OnSelectCoverCardDesign += coverCardDesignVisualizePresenter.SetCoverCardDesign;

        storeGameDesignPresenter.OnOpenGameDesign += selectGameDesignPresenter.SetOpenGameDesign;
        storeGameDesignPresenter.OnCloseGameDesign += selectGameDesignPresenter.SetCloseGameDesign;
        selectGameDesignPresenter.OnChooseCoverCardDesign += storeGameDesignPresenter.SelectGameDesign;
        storeGameDesignPresenter.OnSelectGameDesign += selectGameDesignPresenter.SelectGameDesign;
        storeGameDesignPresenter.OnDeselectGameDesign += selectGameDesignPresenter.DeselectGameDesign;
        storeGameDesignPresenter.OnSelectGameDesign += gameDesignVisualizePresenter.SetGameDesign;
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

        storeCoverCardDesignPresenter.OnOpenCoverCardDesign -= selectCoverCardDesignPresenter.SetOpenCoverCardDesign;
        storeCoverCardDesignPresenter.OnCloseCoverCardDesign -= selectCoverCardDesignPresenter.SetCloseCoverCardDesign;
        selectCoverCardDesignPresenter.OnChooseCoverCardDesign -= storeCoverCardDesignPresenter.SelectCoverCardDesign;
        storeCoverCardDesignPresenter.OnSelectCoverCardDesign -= selectCoverCardDesignPresenter.SelectCoverCardDesign;
        storeCoverCardDesignPresenter.OnDeselectCoverCardDesign -= selectCoverCardDesignPresenter.DeselectCoverCardDesign;
        storeCoverCardDesignPresenter.OnSelectCoverCardDesign -= coverCardDesignVisualizePresenter.SetCoverCardDesign;

        storeGameDesignPresenter.OnOpenGameDesign -= selectGameDesignPresenter.SetOpenGameDesign;
        storeGameDesignPresenter.OnCloseGameDesign -= selectGameDesignPresenter.SetCloseGameDesign;
        selectGameDesignPresenter.OnChooseCoverCardDesign -= storeGameDesignPresenter.SelectGameDesign;
        storeGameDesignPresenter.OnSelectGameDesign -= selectGameDesignPresenter.SelectGameDesign;
        storeGameDesignPresenter.OnDeselectGameDesign -= selectGameDesignPresenter.DeselectGameDesign;
        storeGameDesignPresenter.OnSelectGameDesign -= gameDesignVisualizePresenter.SetGameDesign;
    }

    private void ActivateTransitionsSceneEvents()
    {

    }

    private void DeactivateTransitionsSceneEvents()
    {

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

        gameDesignVisualizePresenter?.Dispose();
        selectGameDesignPresenter?.Dispose();
        coverCardDesignVisualizePresenter?.Dispose();
        selectCoverCardDesignPresenter?.Dispose();
        faceCardDesignVisualizePresenter?.Dispose();
        selectFaceCardDesignPresenter?.Dispose();

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
