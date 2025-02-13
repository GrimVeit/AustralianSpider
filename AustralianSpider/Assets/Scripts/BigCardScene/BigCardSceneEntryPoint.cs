using System;
using UnityEngine;

public class BigCardSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private CardValues cardValues;
    [SerializeField] private BetAmounts amounts;
    [SerializeField] private UIBigCardSceneRoot sceneRootPrefab;

    private UIBigCardSceneRoot sceneRoot;
    private ViewContainer viewContainer;

    private SoundPresenter soundPresenter;
    private ParticleEffectPresenter particleEffectPresenter;

    private TutorialPresenter tutorialPresenter;
    private BankPresenter bankPresenter;

    private CardBetPresenter cardUserBetPresenter;

    private CardMovePresenter cardUserMovePresenter;
    private CardMoveAIPresenter cardMoveAIPresenter;

    private CardSpawnerPresenter cardUserSpawnerPresenter;
    private CardSpawnerPresenter cardAISpawnerPresenter;

    private CardHighlightPresenter cardUserHighlightPresenter;
    private CardHighlightPresenter cardAIHighlightPresenter;

    private CardGamePresenter cardGamePresenter;

    private CardComparisionPresenter cardComparisionPresenter;

    private CardGameWalletPresenter cardGameWalletPresenter;

    private CardHistoryPresenter cardHistoryPresenter;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = Instantiate(sceneRootPrefab);
        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        soundPresenter = new SoundPresenter(new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS), viewContainer.GetView<SoundView>());
        soundPresenter.Initialize();

        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());
        bankPresenter.Initialize();

        tutorialPresenter = new TutorialPresenter(new TutorialModel(), viewContainer.GetView<TutorialView>());
        tutorialPresenter.Initialize();

        cardUserMovePresenter = new CardMovePresenter(new CardMoveModel(tutorialPresenter, soundPresenter), viewContainer.GetView<CardMoveView>());
        cardUserMovePresenter.Initialize();

        cardUserSpawnerPresenter = new CardSpawnerPresenter(new CardSpawnerModel(cardValues), viewContainer.GetView<CardSpawnerView>("User"));
        cardUserSpawnerPresenter.Initialize();

        cardAISpawnerPresenter = new CardSpawnerPresenter(new CardSpawnerModel(cardValues), viewContainer.GetView<CardSpawnerView>("AI"));
        cardAISpawnerPresenter.Initialize();

        cardUserBetPresenter = new CardBetPresenter(new CardBetModel(amounts, tutorialPresenter, soundPresenter), viewContainer.GetView<CardBetView>());
        cardUserBetPresenter.Initialize();

        cardMoveAIPresenter = new CardMoveAIPresenter(new CardMoveAIModel(soundPresenter), viewContainer.GetView<CardMoveAIView>());
        cardMoveAIPresenter.Initialize();

        cardUserHighlightPresenter = new CardHighlightPresenter(new CardHighlightModel(), viewContainer.GetView<CardHighlightView>("User"));
        cardUserHighlightPresenter.Initilize();

        cardAIHighlightPresenter = new CardHighlightPresenter(new CardHighlightModel(), viewContainer.GetView<CardHighlightView>("AI"));
        cardAIHighlightPresenter.Initilize();

        cardGamePresenter = new CardGamePresenter(new CardGameModel(tutorialPresenter, soundPresenter), viewContainer.GetView<CardGameView>());
        cardGamePresenter.Initialize();

        cardComparisionPresenter = new CardComparisionPresenter(new CardComparisionModel(tutorialPresenter, soundPresenter));
        cardComparisionPresenter.Initialize();

        cardGameWalletPresenter = new CardGameWalletPresenter(new CardGameWalletModel(bankPresenter, soundPresenter), viewContainer.GetView<CardGameWalletView>());
        cardGameWalletPresenter.Initialize();

        cardHistoryPresenter = new CardHistoryPresenter(new CardHistoryModel(soundPresenter), viewContainer.GetView<CardHistoryView>());
        cardHistoryPresenter.Initialize();

        cardUserMovePresenter.Activate();

        ActivateComparisedCardEvents();
        ActivateTransferPanelsEvents();
        ActivateActions();

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Initialize();
        sceneRoot.Activate();
    }

    private void ActivateActions()
    {
        cardUserMovePresenter.OnStartMove += cardUserHighlightPresenter.ActivateChooseHighlight;
        cardUserMovePresenter.OnStopMove += cardUserHighlightPresenter.DeactivateChooseHighlight;

        cardUserSpawnerPresenter.OnSpawnCard += cardUserMovePresenter.TeleportBack;
        cardUserSpawnerPresenter.OnSpawnCard += cardUserMovePresenter.Deactivate;
        cardUserSpawnerPresenter.OnSpawnCard += cardUserBetPresenter.Activate;
        cardUserSpawnerPresenter.OnSpawnCard += cardGamePresenter.Reset;

        cardUserBetPresenter.OnSubmitBet += cardUserBetPresenter.Deactivate;
        cardUserBetPresenter.OnSubmitBet += cardGamePresenter.Activate;
        cardUserBetPresenter.OnSubmitBet_Value += cardGameWalletPresenter.SetBet;

        cardGamePresenter.OnChooseChance += cardGamePresenter.Deactivate;
        cardGamePresenter.OnChooseChance += cardMoveAIPresenter.Activate;
        cardGamePresenter.OnChooseChance_Values += cardComparisionPresenter.UserCompare;

        cardMoveAIPresenter.OnStartMove += cardAIHighlightPresenter.ActivateChooseHighlight;
        cardMoveAIPresenter.OnEndMove += cardAIHighlightPresenter.DeactivateChooseHighlight;

        cardMoveAIPresenter.OnEndMove += cardAISpawnerPresenter.SpawnCard;

        cardAISpawnerPresenter.OnSpawnCard += cardMoveAIPresenter.Deactivate;
    }

    private void DeactivateActions()
    {
        cardUserMovePresenter.OnStartMove -= cardUserHighlightPresenter.ActivateChooseHighlight;
        cardUserMovePresenter.OnStopMove -= cardUserHighlightPresenter.DeactivateChooseHighlight;

        cardUserSpawnerPresenter.OnSpawnCard -= cardUserMovePresenter.TeleportBack;
        cardUserSpawnerPresenter.OnSpawnCard -= cardUserMovePresenter.Deactivate;
        cardUserSpawnerPresenter.OnSpawnCard -= cardUserBetPresenter.Activate;
        cardUserSpawnerPresenter.OnSpawnCard -= cardGamePresenter.Reset;

        cardUserBetPresenter.OnSubmitBet -= cardUserBetPresenter.Deactivate;
        cardUserBetPresenter.OnSubmitBet -= cardGamePresenter.Activate;
        cardUserBetPresenter.OnSubmitBet_Value -= cardGameWalletPresenter.SetBet;

        cardGamePresenter.OnChooseChance -= cardGamePresenter.Deactivate;
        cardGamePresenter.OnChooseChance -= cardMoveAIPresenter.Activate;
        cardGamePresenter.OnChooseChance_Values -= cardComparisionPresenter.UserCompare;

        cardMoveAIPresenter.OnStartMove -= cardAIHighlightPresenter.ActivateChooseHighlight;
        cardMoveAIPresenter.OnEndMove -= cardAIHighlightPresenter.DeactivateChooseHighlight;

        cardMoveAIPresenter.OnEndMove -= cardAISpawnerPresenter.SpawnCard;

        cardAISpawnerPresenter.OnSpawnCard -= cardMoveAIPresenter.Deactivate;
    }

    private void ActivateComparisedCardEvents()
    {
        cardUserSpawnerPresenter.OnSpawnCard_Values += cardComparisionPresenter.OnSpawnedCard;
        cardAISpawnerPresenter.OnSpawnCard_Values += cardComparisionPresenter.OnSpawnedCard;

        cardComparisionPresenter.OnSuccessGame += cardGameWalletPresenter.IncreaseMoney;
        cardComparisionPresenter.OnLoseGame += cardGameWalletPresenter.DecreaseMoney;
        cardComparisionPresenter.OnSuccessGame += sceneRoot.OpenSuccessPanel;
        cardComparisionPresenter.OnLoseGame += sceneRoot.OpenLosePanel;

        sceneRoot.OnClickToContinueGameButton_SuccessPanel += cardComparisionPresenter.SubmitGetCards;
        sceneRoot.OnClickToContinueGameButton_LosePanel += cardComparisionPresenter.SubmitGetCards;

        cardComparisionPresenter.OnGetCards_Values += cardHistoryPresenter.AddCardComboHistory;

        cardComparisionPresenter.OnGetCards += cardUserSpawnerPresenter.DestroyCard;
        cardComparisionPresenter.OnGetCards += cardAISpawnerPresenter.DestroyCard;
        cardComparisionPresenter.OnGetCards += cardUserMovePresenter.Activate;
    }

    private void DeactivateComparisedCardEvents()
    {
        cardUserSpawnerPresenter.OnSpawnCard_Values -= cardComparisionPresenter.OnSpawnedCard;
        cardAISpawnerPresenter.OnSpawnCard_Values -= cardComparisionPresenter.OnSpawnedCard;

        cardComparisionPresenter.OnSuccessGame -= cardGameWalletPresenter.IncreaseMoney;
        cardComparisionPresenter.OnLoseGame -= cardGameWalletPresenter.DecreaseMoney;
        cardComparisionPresenter.OnSuccessGame -= sceneRoot.OpenSuccessPanel;
        cardComparisionPresenter.OnLoseGame -= sceneRoot.OpenLosePanel;

        sceneRoot.OnClickToContinueGameButton_SuccessPanel -= cardComparisionPresenter.SubmitGetCards;
        sceneRoot.OnClickToContinueGameButton_LosePanel -= cardComparisionPresenter.SubmitGetCards;

        cardComparisionPresenter.OnGetCards_Values -= cardHistoryPresenter.AddCardComboHistory;

        cardComparisionPresenter.OnGetCards -= cardUserSpawnerPresenter.DestroyCard;
        cardComparisionPresenter.OnGetCards -= cardAISpawnerPresenter.DestroyCard;
        cardComparisionPresenter.OnGetCards -= cardUserMovePresenter.Activate;
    }

    private void ActivateTransferPanelsEvents()
    {
        sceneRoot.OnClickToMoveWinningsButton += sceneRoot.OpenMoveWinningsPanel;
        sceneRoot.OnClickToBacksButton_MoveWinningsPanel += sceneRoot.OpenMainPanel2;

        cardGameWalletPresenter.OnMoneySuccesTransitToBank += sceneRoot.OpenMainPanel2;
        cardGameWalletPresenter.OnMoneySuccesTransitToBank += sceneRoot.OpenMoveMoneyPanel;
        sceneRoot.OnClickToContinueGameButton_MoveMoneyPanel += sceneRoot.OpenMainPanel2;

        sceneRoot.OnClickToContinueGameButton_LosePanel += sceneRoot.OpenMainPanel2;
        sceneRoot.OnClickToContinueGameButton_SuccessPanel += sceneRoot.OpenMainPanel2;

        sceneRoot.OnClickToExitGameButton_LosePanel += HandleGoToMainMenu;
        sceneRoot.OnClickToExitGameButton_SuccessPanel += HandleGoToMainMenu;
        sceneRoot.OnClickToExitGameButton_MoveMoneyPanel += HandleGoToMainMenu;
    }

    private void DeactivateTransferPanelsEvents()
    {
        sceneRoot.OnClickToMoveWinningsButton -= sceneRoot.OpenMoveWinningsPanel;
        sceneRoot.OnClickToBacksButton_MoveWinningsPanel -= sceneRoot.OpenMainPanel2;

        cardGameWalletPresenter.OnMoneySuccesTransitToBank -= sceneRoot.OpenMainPanel2;
        cardGameWalletPresenter.OnMoneySuccesTransitToBank -= sceneRoot.OpenMoveMoneyPanel;
        sceneRoot.OnClickToContinueGameButton_MoveMoneyPanel -= sceneRoot.OpenMainPanel2;

        sceneRoot.OnClickToContinueGameButton_LosePanel -= sceneRoot.OpenMainPanel2;
        sceneRoot.OnClickToContinueGameButton_SuccessPanel -= sceneRoot.OpenMainPanel2;

        sceneRoot.OnClickToExitGameButton_LosePanel -= HandleGoToMainMenu;
        sceneRoot.OnClickToExitGameButton_SuccessPanel -= HandleGoToMainMenu;
        sceneRoot.OnClickToExitGameButton_MoveMoneyPanel -= HandleGoToMainMenu;
    }

    private void Dispose()
    {
        DeactivateActions();
        DeactivateComparisedCardEvents();
        DeactivateTransferPanelsEvents();

        soundPresenter.Dispose();

        tutorialPresenter.Dispose();
        bankPresenter.Dispose();
        cardUserBetPresenter.Dispose();
        cardUserMovePresenter.Dispose();
        cardMoveAIPresenter.Dispose();
        cardUserSpawnerPresenter.Dispose();
        cardAISpawnerPresenter.Dispose();
        cardUserHighlightPresenter.Dispose();
        cardAIHighlightPresenter.Dispose();
        cardGamePresenter.Dispose();
        cardComparisionPresenter.Dispose();
        cardGameWalletPresenter.Dispose();
        cardHistoryPresenter.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region Input Actions

    public event Action GoToMainMenu;

    private void HandleGoToMainMenu()
    {
        Dispose();
        GoToMainMenu?.Invoke();
    }

    #endregion
}
