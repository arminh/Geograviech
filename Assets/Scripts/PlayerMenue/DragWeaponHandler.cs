﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Assets.Scripts.Utils;
using Assets.Scripts;

public class DragWeaponHandler : DragItemHandler  
{
	protected override void OnPlaceInSlot()
	{
        PlayerMenueManager.RemoveActiveWeapon(this.Item as Weapon);
	}
}
