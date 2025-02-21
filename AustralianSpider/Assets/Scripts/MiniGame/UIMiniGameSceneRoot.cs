using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMiniGameSceneRoot : MonoBehaviour
{
    [SerializeField] private HeaderPanel_Game headerPanel;
    [SerializeField] private MainPanel_Game mainPanel;
    [SerializeField] private RestartPanel_Game restartPanel;
    [SerializeField] private ExitPanel_Game exitPanel;
    [SerializeField] private WinPanel_Game winPanel;

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
        restartPanel.Initialize();
        exitPanel.Initialize();
        winPanel.Initialize();
    }

    public void Dispose()
    {
        headerPanel.Dispose();
        mainPanel.Dispose();
        restartPanel.Dispose();
        exitPanel.Dispose();
        winPanel.Dispose();
    }



    public void OpenMainPanel()
    {
        if(mainPanel.IsActive) return;

        OpenPanel(mainPanel);
    }




    public void OpenHeaderPanel()
    {
        if(headerPanel.IsActive) return;

        OpenOtherPanel(headerPanel);
    }

    public void CloseHeaderPanel()
    {
        CloseOtherPanel(headerPanel);
    }






    public void OpenRestartPanel()
    {
        CloseHeaderPanel();

        OpenOtherPanel(restartPanel);
    }

    public void CloseRestartPanel()
    {
        OpenHeaderPanel();

        CloseOtherPanel(restartPanel);
    }





    public void OpenExitPanel()
    {
        CloseHeaderPanel();

        OpenOtherPanel(exitPanel);
    }

    public void CloseExitPanel()
    {
        OpenHeaderPanel();

        CloseOtherPanel(exitPanel);
    }





    public void OpenWinPanel()
    {
        if(winPanel.IsActive) return;

        OpenOtherPanel(winPanel);
    }

    public void CloseWinPanel()
    {
        if(!winPanel.IsActive) return;

        CloseOtherPanel(winPanel);
    }





    public void Activate()
    {
        restartPanel.OnClickToRestart += HandleClickToRestart;
        exitPanel.OnClickToExit += HandleClickToExit;
        winPanel.OnClickToRestart += HandleClickToRestart;
        winPanel.OnClickToExit += HandleClickToExit;

        OpenMainPanel();
        OpenHeaderPanel();
    }

    public void Deactivate()
    {
        restartPanel.OnClickToRestart -= HandleClickToRestart;
        exitPanel.OnClickToExit -= HandleClickToExit;
        winPanel.OnClickToRestart -= HandleClickToRestart;
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

    public event Action OnClickToRestart_Header
    {
        add { headerPanel.OnClickToRestart += value; }
        remove { headerPanel.OnClickToRestart -= value; }
    }

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

    public event Action OnClickToCancel_Restart
    {
        add { restartPanel.OnClickToCancel += value; }
        remove { restartPanel.OnClickToCancel -= value; }
    }





    public event Action OnClickToRestart;
    public event Action OnClickToExit;

    private void HandleClickToRestart()
    {
        OnClickToRestart?.Invoke();
    }

    private void HandleClickToExit()
    {
        OnClickToExit?.Invoke();
    }


    #endregion
}
