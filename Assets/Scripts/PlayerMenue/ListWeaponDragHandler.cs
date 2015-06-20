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

	protected override void OnPlaceInSlot()
	{
		PlayerMenueManager.OnWeaponAdded(this.Item as Weapon);
	}

    public override void OnListItemCreated(object item)
    {
        this.Item = item;
        var weapon = item as Weapon;
        Name.text = string.Format(Name.text, weapon.Name);
        Damage.text = string.Format(Damage.text, weapon.Attack.Damage);
    }
}
