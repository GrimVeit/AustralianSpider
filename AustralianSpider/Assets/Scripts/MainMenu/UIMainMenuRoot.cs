using System;
using UnityEngine;

public class UIMainMenuRoot : MonoBehaviour
{
    private ISoundProvider soundProvider;

    private Panel currentPanel;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {


    }

    public void Activate()
    {

    }



    public void Deactivate()
    {
        currentPanel?.DeactivatePanel();
    }

    public void Dispose()
    {

    }

    

    public void OpenGalaxyInfoPanel()
    {
    }

    public void CloseGalaxyInfoPanel()
    {

    }


    private void OpenPanel(Panel panel)
    {
        if(currentPanel == panel) return;

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



    #endregion
}
