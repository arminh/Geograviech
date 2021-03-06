﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Assets.Scripts.Utils;
using Assets.Scripts.Items.Consumables;
using UnityEngine.UI;

public class DragConsumableHandler : DragItemHandler 
{
    public override void OnPlaceInSlot(ItemSlot slot)
    {
        //TODO something
    }

    public override void OnRemoveFromSlot(ItemSlot slot)
    {
        //TODO something
    }

    public override void OnChangeSlot(ItemSlot fromSlot, ItemSlot toSlot)
    {
        //TODO something
    }

    public override void OnItemCreated(object item)
    {
        this.Item = item;
        var consumable = item as IConsumable;
        var image = transform.GetComponentInChildren<Image>();
        image.sprite = consumable.Icon;
    }
}
