using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Assets.Scripts.Utils;
using Assets.Scripts;

public class DragMonsterHandler : DragItemHandler  
{
	protected override void OnPlaceInSlot(ItemSlot slot)
	{
        PlayerMenueManager.SwapActiveMonster(null, slot.SlotNumber);
	}
}
