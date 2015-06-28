using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Assets.Scripts.Utils;
using Assets.Scripts;

public class ItemSlot : MonoBehaviour, IDropHandler 
{
    public int SlotNumber;
    public Enums.ItemType type;

    public bool IsOccupied()
    {
        return transform.childCount > 0;
    }

    public Transform GetContainedItem()
    {
        return transform.GetChild(0);
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        if (DragItemHandler.ItemToBeDragged.type == type)
        {
            if (IsOccupied())
            {
                var item = GetContainedItem();
                DragItemHandler dragItem = item.GetComponent<DragItemHandler>();
                if (dragItem)
                {
                    dragItem.OnRemoveFromSlot(this);
                }
                Destroy(item.gameObject);
            }

            DragItemHandler.ItemToBeDragged.transform.SetParent(transform);
            DragItemHandler.ItemToBeDragged.transform.localScale = new Vector3(1,1,1);
        }
    }
}
