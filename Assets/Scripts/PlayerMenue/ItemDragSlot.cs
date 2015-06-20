using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Assets.Scripts.Utils;
using Assets.Scripts;

public class ItemDragSlot : MonoBehaviour
{
    public bool IsOccupied()
    {
        return transform.childCount > 0;
    }

    public DragItemHandler GetContainedItem()
    {
        return transform.GetComponentInChildren<DragItemHandler>();
    }
}
