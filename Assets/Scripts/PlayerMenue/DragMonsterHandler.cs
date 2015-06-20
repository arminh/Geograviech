using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Assets.Scripts.Utils;
using Assets.Scripts;

public class DragMonsterHandler : DragItemHandler  
{
	protected override void OnPlaceInSlot()
	{
        PlayerMenueManager.RemoveMonsterFromActive(this.Item as Viech);
	}
}
