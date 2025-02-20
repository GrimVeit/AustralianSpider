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
    }

    public void Dispose()
    {
        headerPanel.Dispose();
        mainPanel.Dispose();
        restartPanel.Dispose();
        exitPanel.Dispose();
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





    public void Activate()
    {
        headerPanel.OnClickToExit += OpenExitPanel;
        headerPanel.OnClickToRestart += OpenRestartPanel;
        restartPanel.OnClickToCancel += CloseRestartPanel;
        exitPanel.OnClickToCancel += CloseExitPanel;

        OpenMainPanel();
        OpenHeaderPanel();
    }

    public void Deactivate()
    {
        headerPanel.OnClickToExit -= OpenExitPanel;
        headerPanel.OnClickToRestart -= OpenRestartPanel;
        restartPanel.OnClickToCancel -= CloseRestartPanel;
        exitPanel.OnClickToCancel -= CloseExitPanel;

        if (currentPanel != null)
           CloseOtherPanel(currentPanel);

        CloseHeaderPanel();
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

    
    public event Action OnClickToRestart
    {
        add { restartPanel.OnClickToRestart += value; }
        remove { restartPanel.OnClickToRestart -= value; }
    }

    public event Action OnClickToExit
    {
        add { exitPanel.OnClickToExit += value; }
        remove { exitPanel.OnClickToExit -= value; }
    }


    #endregion
}
