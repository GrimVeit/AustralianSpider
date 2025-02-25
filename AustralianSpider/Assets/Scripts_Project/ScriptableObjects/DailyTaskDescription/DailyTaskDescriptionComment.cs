using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Comment", menuName = "DailyTask/Comment")]
public class DailyTaskDescriptionComment : ScriptableObject
{
    [SerializeField] private DailyTaskStatus status;
    [SerializeField] private TimePeriod timePeriod;
    [SerializeField] private List<string> comments = new List<string>();

    public DailyTaskStatus Status => status;
    public TimePeriod TimePeriod => timePeriod;
    public List<string> Comments {  get { return comments; } }

    public string GetRandomComment()
    {
        return comments[Random.Range(0, comments.Count)];
    }

}
