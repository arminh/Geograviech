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

    protected Vector3 startPositionMouse;
    protected Vector3 startPositionSlot;

    protected CanvasGroup ItemCanvasGroup;

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        ItemToBeDragged = this;
        ItemOriginalSlot = transform.parent;

        ItemCanvasGroup = GetComponent<CanvasGroup>();
        if (ItemCanvasGroup != null)
            ItemCanvasGroup.blocksRaycasts = false;

        startPositionMouse = Input.mousePosition;
        startPositionSlot = ItemToBeDragged.transform.localPosition;

    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        ItemToBeDragged.transform.localPosition = startPositionSlot + (Input.mousePosition - startPositionMouse);
        //Debug.Log(Input.mousePosition - startPositionMouse);
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        if (ItemCanvasGroup != null)
            ItemCanvasGroup.blocksRaycasts = true;

        if (ItemToBeDragged.transform.parent == ItemOriginalSlot)
        {
            var slot = ItemOriginalSlot.GetComponent<ItemSlot>();
            if (ShouldItemBeDeleted(eventData, ItemOriginalSlot.gameObject))
            {
                Destroy(ItemToBeDragged.gameObject);
                OnRemoveFromSlot(slot);
            }
        }
        else
        {
            var origSlot = ItemOriginalSlot.GetComponent<ItemSlot>();
            var slot = ItemToBeDragged.transform.parent.GetComponent<ItemSlot>();
            if (origSlot && slot && slot.type == this.type && origSlot.type == this.type)
            {
                Debug.Log("OnRemoveFromSlot");
                OnChangeSlot(origSlot, slot);
            }
            else if (slot && slot.type == this.type)
            {
                Debug.Log("DragItemHandler - OnPlaceInSlot");
                OnPlaceInSlot(slot);
            }
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
                Debug.Log("Not Delete Item");
                return false;
            }
        }
        Debug.Log("Delete Item");
        return true;
    }

    public abstract void OnPlaceInSlot(ItemSlot slot);

    public abstract void OnRemoveFromSlot(ItemSlot slot);

    public abstract void OnChangeSlot(ItemSlot fromSlot, ItemSlot toSlot);

    public abstract void OnItemCreated(object item);
}
