using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

using Assets.Scripts;
using Assets.Scripts.Utils;
using Assets.Scripts.Items;


public class DragWeaponHandler : DragItemHandler  
{
    public override void OnPlaceInSlot(ItemSlot slot)
	{
        PlayerMenueManager.SetActiveWeapon(Item as Weapon);
	}

    public override void OnRemoveFromSlot(ItemSlot slot)
    {
        PlayerMenueManager.RemoveActiveWeapon(Item as Weapon);
    }

    public override void OnChangeSlot(ItemSlot fromSlot, ItemSlot toSlot)
    {
        //TODO something
    }

    public override void OnItemCreated(object item)
    {
        this.Item = item;
        var weapon = this.Item as Weapon;
        var image = transform.GetComponentInChildren<Image>();
        image.sprite = weapon.Icon;
        Name = weapon.Name;
    }
}
