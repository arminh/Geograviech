using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using Assets.Scripts;
using Assets.Scripts.Utils;
using Assets.Scripts.Character;

public class DragMonsterHandler : DragItemHandler  
{
    public override void OnPlaceInSlot(ItemSlot slot)
	{
        PlayerMenueManager.AddActiveMonster(Item as Viech, slot.SlotNumber);
	}

    public override void OnRemoveFromSlot(ItemSlot slot)
    {
        PlayerMenueManager.RemoveActiveMonster(Item as Viech, slot.SlotNumber);
    }

    public override void OnChangeSlot(ItemSlot fromSlot, ItemSlot toSlot)
    {
        PlayerMenueManager.ChangeSlotActiveMonster(fromSlot.SlotNumber, toSlot.SlotNumber, Item as Viech);
    }

    public override void OnItemCreated(object item)
    {
        this.Item = item;
        var monster = this.Item as Viech;
        var image = transform.GetComponentInChildren<Image>();
        image.sprite = monster.Icon;
    }
}
