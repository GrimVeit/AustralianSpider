using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMiniGameSceneRoot : MonoBehaviour
{
    [SerializeField] private HeaderPanel_Game headerPanel;
    [SerializeField] private MainPanel_Game mainPanel;

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
    }

    public void Dispose()
    {
        headerPanel.Dispose();
        mainPanel.Dispose();
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



    public void Activate()
    {
        OpenMainPanel();
        OpenHeaderPanel();
    }

    public void Deactivate()
    {
        if(currentPanel != null)
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
        add { headerPanel.OnClickToRestart += value; }
        remove { headerPanel.OnClickToRestart -= value; }
    }

    public event Action OnClickToExit
    {
        add { headerPanel.OnClickToExit += value; }
        remove { headerPanel.OnClickToExit -= value; }
    }


    #endregion
}
