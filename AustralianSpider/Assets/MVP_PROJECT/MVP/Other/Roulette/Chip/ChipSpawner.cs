using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChipSpawner : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private Transform canvas;
    public event Action<Chip, PointerEventData> OnSpawnChip;

    [SerializeField] private Chip chipPrefab;

    private Chip chip;
    private bool isActive = true;

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isActive) return;

        chip = Instantiate(chipPrefab, canvas.transform);
        chip.transform.SetPositionAndRotation(transform.position, chipPrefab.transform.rotation);
        OnSpawnChip?.Invoke(chip, eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //throw new NotImplementedException();
    }
}
