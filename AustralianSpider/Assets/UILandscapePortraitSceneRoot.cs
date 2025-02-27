using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILandscapePortraitSceneRoot : MonoBehaviour
{
    private Panel currentPanel;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {

    }

    public void Initialize()
    {

    }

    public void Activate()
    {

    }



    public void Deactivate()
    {

    }

    public void Dispose()
    {

    }

    #region Base

    private void OpenPanel(Panel panel)
    {
        if (currentPanel == panel) return;

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

    #endregion
}
