﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Assets.Scripts.Utils;
using Assets.Scripts;
using UnityEngine.UI;
using Assets;

public class ListMonsterDragHandler : ListItemDragHandler
{
    public Text Name;
    public Text TypeLvl;

	protected override void OnPlaceInSlot(ItemSlot slot)
	{
        PlayerMenueManager.SwapActiveMonster(this.Item as Viech, slot.SlotNumber);
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
        PlayerMenueManager.SwitchMonsterPlayerPanel();
        PlayerMenueManager.SetMonsterPanelInformation(this.Item as Viech);
    }
}
