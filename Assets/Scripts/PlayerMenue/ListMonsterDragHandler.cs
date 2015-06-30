using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using Assets;
using Assets.Scripts;
using Assets.Scripts.Utils;
using Assets.Scripts.Character;
using Assets.Scripts.Effects;

public class ListMonsterDragHandler : ListItemDragHandler
{
    public Text Name;
    public Text TypeLvl;

    public override void OnPlaceInSlot(ItemSlot slot)
    {
        Debug.Log("ListMonsterDragHandler - OnPlaceInSlot");
        PlayerMenueManager.AddActiveMonster(Item as Viech, slot.SlotNumber);
    }

    public override void OnItemCreated(object item)
    {
        this.Item = item;
        var monster = item as Viech;
        var image = transform.GetComponentInChildren<Image>();
        image.sprite = monster.Icon;
        Name.text = string.Format(Name.text, monster.Name);
        TypeLvl.text = string.Format(TypeLvl.text, monster.Type.ToString(), monster.Level);
    }

    public void OnSingleShortClick()
    {
        PlayerMenueManager.SwitchToMonsterPanel();
        PlayerMenueManager.SetMonsterPanelInformation(this.Item as Viech);
    }
}
