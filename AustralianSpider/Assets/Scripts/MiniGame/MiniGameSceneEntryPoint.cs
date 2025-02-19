using System;
using UnityEngine;

public class MiniGameSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private GameDesignGroup gameDesignGroup;
    [SerializeField] private UIMiniGameSceneRoot sceneRootPrefab;

    private UIMiniGameSceneRoot sceneRoot;
    private ViewContainer viewContainer;

    private SoundPresenter soundPresenter;
    private ParticleEffectPresenter particleEffectPresenter;
    private BankPresenter bankPresenter;

    private StoreGameDesignPresenter storeGameDesignPresenter;
    private GameDesignPresenter gameDesignPresenter;

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

        ActivateEvents();

        gameDesignPresenter.Initialize();
        storeGameDesignPresenter.Initialize();
    }

    private void ActivateEvents()
    {
        ActivateTransitionEvents();

        storeGameDesignPresenter.OnSelectGameDesign += gameDesignPresenter.SetGameDesign;
    }

    private void DeactivateEvents()
    {
        DeactivateTransitionEvents();

        storeGameDesignPresenter.OnSelectGameDesign -= gameDesignPresenter.SetGameDesign;
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

        gameDesignPresenter?.Dispose();
        storeGameDesignPresenter?.Dispose();
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
        OnGoToMainMenu?.Invoke();
    }

    #endregion
}
