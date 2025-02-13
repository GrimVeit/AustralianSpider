using System;
using UnityEngine;

public class UIRouletteRoot : MonoBehaviour
{
    [SerializeField] private MainPanel_RouletteScene mainPanel;
    [SerializeField] private SpinPanel_RouletteScene spinPanel;

    private ISoundProvider soundProvider;
    //private IParticleEffectProvider particleEffectProvider;

    private Panel currentPanel;

    public void Initialize()
    {
        mainPanel.SetSoundProvider(soundProvider);

        mainPanel.Initialize();
        spinPanel.Initialize();
    }

    public void Activate()
    {
        OpenMainPanel();
    }

    public void Deactivate()
    {
        mainPanel.Dispose();
        spinPanel.Dispose();
    }

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void SetParticleEffectProvider(IParticleEffectProvider particleEffectProvider)
    {
        //this.particleEffectProvider = particleEffectProvider;
    }

    public void Dispose()
    {

    }

    public void OpenMainPanel()
    {
        OpenPanel(mainPanel);
    }

    public void OpenSpinPanel()
    {
        OpenOtherPanel(spinPanel);
    }

    public void CloseSpinPanel()
    {
        CloseOtherPanel(spinPanel);
    }




    private void OpenPanel(Panel panel)
    {
        if (currentPanel != null)
            currentPanel.DeactivatePanel();

        //soundProvider.PlayOneShot("ShoohPanel_Open");
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

    public event Action OnClickToBackButton
    {
        add { mainPanel.OnClickToBackButton += value; }
        remove { mainPanel.OnClickToBackButton -= value; }
    }

    public event Action OnClickToSpinButton
    {
        add { mainPanel.OnClickToSpinButton += value; }
        remove { mainPanel.OnClickToSpinButton -= value; }
    }

    #endregion
}
