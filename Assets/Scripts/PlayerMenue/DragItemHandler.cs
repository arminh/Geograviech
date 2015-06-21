using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Assets.Scripts.Utils;
using System.Collections.Generic;

public abstract class DragItemHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler 
{
    public Enums.ItemType type;
	public object Item;

    public static DragItemHandler ItemToBeDragged;
    public static Transform ItemOriginalSlot;

    protected CanvasGroup ItemCanvasGroup;

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        ItemToBeDragged = this;
        ItemOriginalSlot = transform.parent;

        ItemCanvasGroup = GetComponent<CanvasGroup>();
        if (ItemCanvasGroup != null)
            ItemCanvasGroup.blocksRaycasts = false;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        ItemToBeDragged.transform.position = Input.mousePosition;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        if (ItemCanvasGroup != null)
            ItemCanvasGroup.blocksRaycasts = true;

        if (ItemToBeDragged.transform.parent == ItemOriginalSlot)
        {
            var slot = ItemOriginalSlot.GetComponent<ItemSlot>();
            if (slot && slot.type == this.type)
            {
                OnPlaceInSlot(slot);    
            }

            if (ShouldItemBeDeleted(eventData, ItemOriginalSlot.gameObject))
                Destroy(ItemToBeDragged.gameObject);
        }

        ItemToBeDragged = null;
    }

    private bool ShouldItemBeDeleted(PointerEventData eventData, GameObject slot)
    {
        EventSystem events = FindObjectOfType<EventSystem>();
        List<RaycastResult> result = new List<RaycastResult>();
        events.RaycastAll(eventData, result);
        foreach (RaycastResult res in result)
        {
            if(res.gameObject == slot)
            {
                return false;
            }
        }
        return true;
    }

	protected abstract void OnPlaceInSlot(ItemSlot slot);

    public abstract void OnItemCreated(object item);
}
