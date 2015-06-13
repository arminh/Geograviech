using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Assets.Scripts.Utils;

public class ItemSlot : MonoBehaviour, IDropHandler 
{
    public Enums.ItemType type;

    public bool IsOccupied()
    {
        return transform.childCount > 0;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!IsOccupied() && DragItemHandler.ItemToBeDragged.type == type)
        {
            DragItemHandler.ItemToBeDragged.transform.SetParent(transform);
            DragItemHandler.ItemToBeDragged.transform.localScale = new Vector3(1,1,1);
        }
    }
}
