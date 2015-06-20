using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Assets.Scripts.Utils;
using Assets.Scripts;
using UnityEngine.UI;

public class ListWeaponDragHandler : ListItemDragHandler
{
    public Text Name;
    public Text Damage;

	protected override void OnPlaceInSlot(ItemSlot slot)
	{
		PlayerMenueManager.SwapActiveWeapon(this.Item as Weapon);
	}

    public override void OnItemCreated(object item)
    {
        this.Item = item;
        var weapon = item as Weapon;
        var image = transform.GetComponentInChildren<Image>();
        image.sprite = weapon.Icon;
        Name.text = string.Format(Name.text, weapon.Name);
        Damage.text = string.Format(Damage.text, weapon.Attack.Damage);
    }
}
