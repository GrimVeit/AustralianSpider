using UnityEngine;

public class RouletteSlotValue : MonoBehaviour
{
    [SerializeField] private RouletteNumber rouletteNumber;
    [SerializeField] private Transform slotTransform;
    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform endTransform;

    public Transform SlotTransform => slotTransform;
    public Transform StartTransform => startTransform;
    public Transform EndTransform => endTransform;
    public RouletteNumber RouletteNumber => rouletteNumber;
}
