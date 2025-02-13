using System;
using UnityEngine;

public class UIBigCardSceneRoot : MonoBehaviour
{
    [SerializeField] private MainPanel_BigCardScene mainPanel;
    [SerializeField] private MainPanel2_BigCardScene mainPanel2;
    [SerializeField] private MoveWinningsPanel_BigCardScene moveWinningsPanel;
    [SerializeField] private MoveMoneyPanel_BigCardScene moveMoneyPanel;
    [SerializeField] private SuccessPanel_BigCardScene successPanel;
    [SerializeField] private LosePanel_BigCardScene losePanel;

    private ISoundProvider soundProvider;

    private Panel currentPanel;

    public void Initialize()
    {
        mainPanel.SetSoundProvider(soundProvider);
        moveWinningsPanel.SetSoundProvider(soundProvider);
        moveMoneyPanel.SetSoundProvider(soundProvider);
        successPanel.SetSoundProvider(soundProvider);
        losePanel.SetSoundProvider(soundProvider);

        mainPanel.Initialize();
        mainPanel2.Initialize();
        moveWinningsPanel.Initialize();
        moveMoneyPanel.Initialize();
        successPanel.Initialize();
        losePanel.Initialize();
    }

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void Dispose()
    {
        mainPanel.Dispose();
        mainPanel2.Dispose();
        moveWinningsPanel.Dispose();
        moveMoneyPanel.Dispose();
        successPanel.Dispose();
        losePanel.Dispose();
    }

    public void Activate()
    {
        OpenOtherPanel(mainPanel);
        OpenMainPanel2();
    }

    public void Deactivate()
    {
        ClosePanel();
    }

    public void OpenMainPanel2()
    {
        OpenPanel(mainPanel2);
    }

    public void OpenMoveWinningsPanel()
    {
        OpenPanel(moveWinningsPanel);
    }

    public void OpenMoveMoneyPanel()
    {
        OpenPanel(moveMoneyPanel);
    }

    public void OpenSuccessPanel()
    {
        OpenPanel(successPanel);
    }

    public void OpenLosePanel()
    {
        OpenPanel(losePanel);
    }

    public void OpenPanel(Panel panel)
    {
        currentPanel?.DeactivatePanel();

        currentPanel = panel;
        currentPanel.ActivatePanel();
    }

    public void ClosePanel()
    {
        currentPanel?.DeactivatePanel();
    }

    public void OpenOtherPanel(Panel panel)
    {
        panel.ActivatePanel();
    }

    public void CloseOtherPanel(Panel panel)
    {
        panel.DeactivatePanel();
    }

    #region Input

    public event Action OnClickToBackButton_MainPanel
    {
        add { mainPanel.OnClickToBackButton += value; }
        remove { mainPanel.OnClickToBackButton -= value; }
    }

    public event Action OnClickToMoveWinningsButton
    {
        add { mainPanel.OnClickToMoveWinningsButton += value; }
        remove { mainPanel.OnClickToMoveWinningsButton -= value; }
    }

    public event Action OnClickToBacksButton_MoveWinningsPanel
    {
        add { moveWinningsPanel.OnClickToBackButton += value; }
        remove { moveWinningsPanel.OnClickToBackButton -= value; }
    }

    public event Action OnClickToContinueGameButton_MoveMoneyPanel
    {
        add { moveMoneyPanel.OnClickToContinueButton += value; }
        remove { moveMoneyPanel.OnClickToContinueButton -= value; }
    }

    public event Action OnClickToContinueGameButton_SuccessPanel
    {
        add { successPanel.OnClickToContinueButton += value; }
        remove { successPanel.OnClickToContinueButton -= value; }
    }

    public event Action OnClickToExitGameButton_SuccessPanel
    {
        add { successPanel.OnClickToExitButton += value; }
        remove { successPanel.OnClickToExitButton -= value; }
    }

    public event Action OnClickToContinueGameButton_LosePanel
    {
        add { losePanel.OnClickToContinueButton += value; }
        remove { losePanel.OnClickToContinueButton -= value; }
    }

    public event Action OnClickToExitGameButton_LosePanel
    {
        add { losePanel.OnClickToExitButton += value; }
        remove { losePanel.OnClickToExitButton -= value; }
    }

    public event Action OnClickToExitGameButton_MoveMoneyPanel
    {
        add { moveMoneyPanel.OnClickToExitButton += value; }
        remove { moveMoneyPanel.OnClickToExitButton -= value; }
    }

    #endregion
}
