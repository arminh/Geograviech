using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using Assets.Scripts;
using Assets.Scripts.Utils;
using Assets.Scripts.Character;

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
