using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DailyTaskDescriptionView : View
{
    [SerializeField] private TextMeshProUGUI textDescription;

    public void SetDescription(string description)
    {
        textDescription.text = description;
    }
}
