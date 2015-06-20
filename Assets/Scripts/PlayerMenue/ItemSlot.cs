﻿using UnityEngine;
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

    public virtual void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (DragItemHandler.ItemToBeDragged.type == type)
        {
            Debug.Log("DropIT");
            if(IsOccupied())
            {
                transform.DetachChildren();
            }
            DragItemHandler.ItemToBeDragged.transform.SetParent(transform);
            DragItemHandler.ItemToBeDragged.transform.localScale = new Vector3(1,1,1);
        }
    }
}
