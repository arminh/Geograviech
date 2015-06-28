using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

using Assets.Scripts;
using Assets.Scripts.Utils;
using Assets.Scripts.Items;


public class DragWeaponHandler : DragItemHandler  
{
	protected override void OnPlaceInSlot(ItemSlot slot)
	{
        PlayerMenueManager.SwapActiveWeapon(null);
	}

    public override void OnItemCreated(object item)
    {
        this.Item = item;
        var weapon = this.Item as Weapon;
        var image = transform.GetComponentInChildren<Image>();
        image.sprite = weapon.Icon;
    }
}
