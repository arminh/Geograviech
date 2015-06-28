using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using Assets.Scripts;
using Assets.Scripts.Utils;
using Assets.Scripts.Items;

public class ListWeaponDragHandler : ListItemDragHandler
{
    public Text Name;
    public Text Damage;

    public override void OnItemCreated(object item)
    {
        this.Item = item;
        var weapon = item as Weapon;
        var image = transform.GetComponentInChildren<Image>();
        image.sprite = weapon.Icon;
        Name.text = string.Format(Name.text, weapon.Name);
        Damage.text = string.Format(Damage.text, weapon.Attack.MinDamage, weapon.Attack.MaxDamage);
    }
}
