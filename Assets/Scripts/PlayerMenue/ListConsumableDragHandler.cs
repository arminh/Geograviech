using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using Assets.Scripts;
using Assets.Scripts.Utils;
using Assets.Scripts.Items.Consumables;


public class ListConsumableDragHandler : ListItemDragHandler
{
    public Text Name;
    public Text Effect;

    public override void OnItemCreated(object item)
    {
        this.Item = item;
        var consumable = item as IConsumable;
        var image = transform.GetComponentInChildren<Image>();
        image.sprite = consumable.Icon;
        Name.text = string.Format(Name.text, consumable.Name);
        Effect.text = string.Format(Effect.text, consumable.Description);
    }
}
