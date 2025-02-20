using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MotionCounterView : View
{
    [SerializeField] private List<TextMeshProUGUI> textCountMotions = new List<TextMeshProUGUI>();

    public void VisualizeCountMotions(int count)
    {
        textCountMotions.ForEach(text => text.text = count.ToString());
    }
}
