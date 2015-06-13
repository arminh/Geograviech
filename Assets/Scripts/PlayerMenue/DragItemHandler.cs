﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Assets.Scripts.Utils;

public class DragItemHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler 
{
    public Enums.ItemType type;

    public static DragItemHandler ItemToBeDragged;
    public static Transform ItemOriginalSlot;

    protected CanvasGroup ItemCanvasGroup;

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("begin");

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
        Debug.Log("EndDrag");

        if (ItemCanvasGroup != null)
            ItemCanvasGroup.blocksRaycasts = true;

        if (transform.parent == ItemOriginalSlot)
            Destroy(ItemToBeDragged.gameObject);

        ItemToBeDragged = null;
    }
}