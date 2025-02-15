using System;
using UnityEngine;
using UnityEngine.UI;

public class ChooseTypeDesignPanel_Menu : MovePanel
{
    [SerializeField] private Button buttonBack;

    [SerializeField] private Button buttonCoverDesign;
    [SerializeField] private Button buttonFaceDesign;
    [SerializeField] private Button buttonGameDesign;

    public override void Initialize()
    {
        base.Initialize();

        buttonBack.onClick.AddListener(() => OnClickToBack?.Invoke());

        buttonCoverDesign.onClick.AddListener(() => OnClickToCoverDesign?.Invoke());
        buttonFaceDesign.onClick.AddListener(() => OnClickToFaceDesign?.Invoke());
        buttonGameDesign.onClick.AddListener(() => OnClickToGameDesign?.Invoke());
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonBack.onClick.RemoveListener(() => OnClickToBack?.Invoke());

        buttonCoverDesign.onClick.RemoveListener(() => OnClickToCoverDesign?.Invoke());
        buttonFaceDesign.onClick.RemoveListener(() => OnClickToFaceDesign?.Invoke());
        buttonGameDesign.onClick.RemoveListener(() => OnClickToGameDesign?.Invoke());
    }

    #region Input

    public event Action OnClickToBack;

    public event Action OnClickToCoverDesign;
    public event Action OnClickToFaceDesign;
    public event Action OnClickToGameDesign;

    #endregion
}
