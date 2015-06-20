using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Assets.Scripts.Utils;
using Assets.Scripts.Consumables;
using UnityEngine.UI;

public class DragConsumableHandler : DragItemHandler 
{
    protected override void OnPlaceInSlot(ItemSlot slot)
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
