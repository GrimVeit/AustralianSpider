using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDailyTaskGameSceneRoot : MonoBehaviour
{
    [SerializeField] private HeaderPanel_DailyTaskGame headerPanel;
    [SerializeField] private MainPanel_Game mainPanel;
    [SerializeField] private ExitPanel_DailyTaskGame exitPanel;
    [SerializeField] private WinPanel_DailyTaskGame winPanel;

    private ISoundProvider soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    private Panel currentPanel;

    public void Initialize()
    {
        headerPanel.Initialize();
        mainPanel.Initialize();
        exitPanel.Initialize();
        winPanel.Initialize();
    }

    public void Dispose()
    {
        headerPanel.Dispose();
        mainPanel.Dispose();
        exitPanel.Dispose();
        winPanel.Dispose();
    }



    public void OpenMainPanel()
    {
        if (mainPanel.IsActive) return;

        OpenPanel(mainPanel);
    }




    public void OpenHeaderPanel()
    {
        if (headerPanel.IsActive) return;

        OpenOtherPanel(headerPanel);
    }

    public void CloseHeaderPanel()
    {
        if (!headerPanel.IsActive) return;

        CloseOtherPanel(headerPanel);
    }






    public void OpenExitPanel()
    {
        if (exitPanel.IsActive) return;

        OpenOtherPanel(exitPanel);
    }

    public void CloseExitPanel()
    {
        if (!exitPanel.IsActive) return;

        CloseOtherPanel(exitPanel);
    }





    public void OpenWinPanel()
    {
        if (winPanel.IsActive) return;

        OpenOtherPanel(winPanel);
    }

    public void CloseWinPanel()
    {
        if (!winPanel.IsActive) return;

        CloseOtherPanel(winPanel);
    }





    public void Activate()
    {
        exitPanel.OnClickToExit += HandleClickToExit;
        winPanel.OnClickToExit += HandleClickToExit;

        OpenMainPanel();
        OpenHeaderPanel();
    }

    public void Deactivate()
    {
        exitPanel.OnClickToExit -= HandleClickToExit;
        winPanel.OnClickToExit -= HandleClickToExit;

        if (currentPanel != null)
            CloseOtherPanel(currentPanel);

        CloseWinPanel();
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

    #region Input

    public event Action OnClickToExit_Header
    {
        add { headerPanel.OnClickToExit += value; }
        remove { headerPanel.OnClickToExit -= value; }
    }

    public event Action OnClickToCancel_Exit
    {
        add { exitPanel.OnClickToCancel += value; }
        remove { exitPanel.OnClickToCancel -= value; }
    }





    public event Action OnClickToExit;

    private void HandleClickToExit()
    {
        OnClickToExit?.Invoke();
    }


    #endregion
}
