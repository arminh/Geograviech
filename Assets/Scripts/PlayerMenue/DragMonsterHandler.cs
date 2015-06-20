using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Assets.Scripts.Utils;
using Assets.Scripts;
using UnityEngine.UI;

public class DragMonsterHandler : DragItemHandler  
{
	protected override void OnPlaceInSlot(ItemSlot slot)
	{
        PlayerMenueManager.SwapActiveMonster(null, slot.SlotNumber);
	}

    public override void OnItemCreated(object item)
    {
        this.Item = item;
        var monster = this.Item as Viech;
        var image = transform.GetComponentInChildren<Image>();
        image.sprite = monster.Icon;
    }
}
