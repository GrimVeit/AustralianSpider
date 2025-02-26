using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "CommentGroup", menuName = "DailyTask/CommentGroup")]
public class DailyTaskDescriptionComments : ScriptableObject
{
    [SerializeField] private List<DailyTaskDescriptionComment> comments = new List<DailyTaskDescriptionComment>();

    public string GetRandomCommentByStatusAndTime(DailyTaskStatus status, TimePeriod timePeriod)
    {
        Debug.Log(status + "//" + timePeriod);

        return comments.FirstOrDefault(data => data.Status == status && data.TimePeriod == timePeriod).GetRandomComment();
    }
}
