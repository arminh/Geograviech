using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Assets.Scripts.Utils;
using Assets.Scripts;

public class DragMonsterHandler : DragItemHandler  
{
	protected override void OnPlaceInSlot()
	{
        PlayerMenueManager.OnMonsterRemoved(this.Item as Viech);
	}
}
