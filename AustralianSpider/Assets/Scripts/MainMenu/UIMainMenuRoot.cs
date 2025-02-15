using System;
using UnityEngine;

public class UIMainMenuRoot : MonoBehaviour
{
    [SerializeField] private MainPanel_Menu mainPanel;
    [SerializeField] private DailyTaskPanel_Menu dailyTaskPanel;
    [SerializeField] private ChooseTypeDesignPanel_Menu chooseTypeDesignPanel;
    [SerializeField] private VisualizeDesignPanel_Menu visualizeDesignPanel;
    [SerializeField] private CoverDesignPanel_Menu coverDesignPanel;
    [SerializeField] private FaceDesignPanel_Menu faceDesignPanel;
    [SerializeField] private BackgroundDesignPanel_Menu backgroundDesignPanel;

    private ISoundProvider soundProvider;

    private Panel currentPanel;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {
        mainPanel.Initialize();
        dailyTaskPanel.Initialize();
        chooseTypeDesignPanel.Initialize();
        coverDesignPanel.Initialize();
        backgroundDesignPanel.Initialize();
        faceDesignPanel.Initialize();
        visualizeDesignPanel.Initialize();
    }

    public void Activate()
    {
        mainPanel.OnClickToDailyTask += OpenDailyTaskPanel;
        mainPanel.OnClickToDesign += OpenChooseTypeDesignPanel;

        dailyTaskPanel.OnClickToBack += OpenMainPanel;

        chooseTypeDesignPanel.OnClickToBack += OpenMainPanel;
        chooseTypeDesignPanel.OnClickToCoverDesign += OpenCoverDesignPanel;
        chooseTypeDesignPanel.OnClickToFaceDesign += OpenFaceDesignPanel;
        chooseTypeDesignPanel.OnClickToGameDesign += OpenGameDesignPanel;

        coverDesignPanel.OnClickToBack += OpenChooseTypeDesignPanel;
        faceDesignPanel.OnClickToBack += OpenChooseTypeDesignPanel;
        backgroundDesignPanel.OnClickToBack += OpenChooseTypeDesignPanel;

        OpenMainPanel();
    }



    public void Deactivate()
    {
        mainPanel.OnClickToDailyTask -= OpenDailyTaskPanel;
        mainPanel.OnClickToDesign -= OpenChooseTypeDesignPanel;

        dailyTaskPanel.OnClickToBack -= OpenMainPanel;

        chooseTypeDesignPanel.OnClickToBack -= OpenMainPanel;
        chooseTypeDesignPanel.OnClickToCoverDesign -= OpenCoverDesignPanel;
        chooseTypeDesignPanel.OnClickToFaceDesign -= OpenFaceDesignPanel;
        chooseTypeDesignPanel.OnClickToGameDesign -= OpenGameDesignPanel;

        coverDesignPanel.OnClickToBack -= OpenChooseTypeDesignPanel;
        faceDesignPanel.OnClickToBack -= OpenChooseTypeDesignPanel;
        backgroundDesignPanel.OnClickToBack -= OpenChooseTypeDesignPanel;
    }

    public void Dispose()
    {
        mainPanel.Dispose();
        dailyTaskPanel.Dispose();
        chooseTypeDesignPanel.Dispose();
        coverDesignPanel.Dispose();
        backgroundDesignPanel.Dispose();
        faceDesignPanel.Dispose();
        visualizeDesignPanel.Dispose();
    }


    public void OpenMainPanel()
    {
        CloseVisualizeDesignPanel();

        OpenPanel(mainPanel);
    }


    public void OpenDailyTaskPanel()
    {
        OpenPanel(dailyTaskPanel);
    }


    public void OpenChooseTypeDesignPanel()
    {
        OpenVisualizeDesignPanel();

        OpenPanel(chooseTypeDesignPanel);
    }

    public void OpenCoverDesignPanel()
    {
        OpenPanel(coverDesignPanel);
    }

    public void OpenFaceDesignPanel()
    {
        OpenPanel(faceDesignPanel);
    }

    public void OpenGameDesignPanel()
    {
        OpenPanel(backgroundDesignPanel);
    }



    public void OpenVisualizeDesignPanel()
    {
        if (visualizeDesignPanel.IsActive) return;

        OpenOtherPanel(visualizeDesignPanel);
    }

    public void CloseVisualizeDesignPanel()
    {
        if(!visualizeDesignPanel.IsActive) return;

        CloseOtherPanel(visualizeDesignPanel);
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

    #region Input Actions

    public event Action OnPlay
    {
        add { mainPanel.OnClickToPlay += value; }
        remove { mainPanel.OnClickToPlay -= value; }
    }


    #endregion
}
