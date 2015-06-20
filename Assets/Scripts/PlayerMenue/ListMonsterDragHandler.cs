using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Assets.Scripts.Utils;
using Assets.Scripts;
using UnityEngine.UI;

public class ListMonsterDragHandler : ListItemDragHandler
{
    public Text Name;
    public Text TypeLvl;

	protected override void OnPlaceInSlot()
	{
		PlayerMenueManager.AddMonsterToActive(this.Item as Viech);
	}

    public override void OnListItemCreated(object item)
    {
        this.Item = item;
        var monster = item as Viech;
        Name.text = string.Format(Name.text, monster.Name);
        TypeLvl.text = string.Format(TypeLvl.text, monster.Type.ToString(), monster.Level);
    }

    public void OnSingleShortClick()
    {
        PlayerMenueManager.SwitchMonsterPlayerPanel();
        PlayerMenueManager.SetMonsterPanelInformation(this.Item as Viech);
    }
}
