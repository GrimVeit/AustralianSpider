using System;
using UnityEngine;

public class UIGameSoloRoot : MonoBehaviour
{
    [SerializeField] private MainPanel_GameSoloScene mainPanel;
    [SerializeField] private FinishPanel_GameSoloScene finishPanel;

    [SerializeField] private RollPanel_GameSoloScene rollPanel;
    [SerializeField] private RollPlayPanel_GameSoloScene rollPlayPanel;

    private ISoundProvider soundProvider;

    private Panel currentPanel;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {
        mainPanel.Initialize();
        rollPlayPanel.Initialize();
        rollPanel.Initialize();
        finishPanel.Initialize();
    }

    public void Activate()
    {
        OpenMainPanel();
        OpenRollPanel();
    }

    public void Deactivate()
    {
        currentPanel.DeactivatePanel();
    }

    public void Dispose()
    {
        mainPanel.Dispose();
        rollPanel.Dispose();
        rollPlayPanel.Dispose();
        finishPanel.Dispose();
    }

    public void OpenMainPanel()
    {
        OpenPanel(mainPanel);
    }

    public void OpenFinishPanel()
    {
        soundProvider.PlayOneShot("Win");
        OpenPanel(finishPanel);
    }

    public void OpenRollPanel()
    {
        if (rollPlayPanel.IsActivePanel)
        {
            CloseOtherPanel(rollPlayPanel);
        }

        OpenOtherPanel(rollPanel);
    }

    public void OpenPlayRollPanel()
    {
        if (rollPanel.IsActivePanel)
        {
            CloseOtherPanel(rollPanel);
        }

        OpenOtherPanel(rollPlayPanel);
    }


    private void OpenPanel(Panel panel)
    {
        if (currentPanel != null)
            currentPanel.DeactivatePanel();

        currentPanel = panel;
        currentPanel.ActivatePanel();

    }

    private void OpenOtherPanel(Panel panel)
    {
        panel.ActivatePanel();
    }

    private void CloseOtherPanel(Panel panel)
    {
        panel.DeactivatePanel();
    }


    #region Input Actions

    public event Action OnClickToGoMainMenuFromMainPanel
    {
        add { mainPanel.OnClickToGoMainMenu += value; }
        remove { mainPanel.OnClickToGoMainMenu -= value; }
    }

    public event Action OnClickToGoMainMenuFromFinishPanel
    {
        add { finishPanel.OnGoToMainMenu += value; }
        remove { finishPanel.OnGoToMainMenu -= value; }
    }

    public event Action OnClickToGoSoloGameFromFinishPanel
    {
        add { finishPanel.OnGoToSoloGame += value; }
        remove { finishPanel.OnGoToSoloGame -= value; }
    }

    #endregion
}
