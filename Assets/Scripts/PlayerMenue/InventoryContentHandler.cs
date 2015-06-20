﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Consumables;
using System.Linq;
using UnityEngine.UI;

public class InventoryContentHandler : MonoBehaviour 
{
    public Transform MonsterList;
    public Transform ItemList;
    public Transform WeaponList;

	// Use this for initialization
	void Start () 
    {
        var manager = FindObjectOfType<PlayerMenueManager>();
        var monsters = manager.GetAllMonstersOfPlayer();
        var items = manager.GetAllConsumablesOfPlayer();
        var weapons = manager.GetAllWeaponsOfPlayer();

        FillMonsterList(manager.GetAllMonstersOfPlayer());
        FillItemList(manager.GetAllConsumablesOfPlayer());  
        FillWeaponList(manager.GetAllWeaponsOfPlayer());      
	}

    private void FillMonsterList(List<Viech> monsters)
    {
        foreach (var monster in monsters)
        {
            AddMonsterToList(monster);
        }
    }

    public void AddMonsterToList(Viech monster)
    {
        var prefab = PrefabContainer.getMonsterListIconPrefab();
        var image = prefab.GetComponentInChildren<Image>();
        image.sprite = monster.Icon;
        var dragHandler = prefab.GetComponent<ListMonsterDragHandler>();
        dragHandler.OnListItemCreated(monster);
        prefab.transform.SetParent(MonsterList);
    }

    private void FillItemList(List<IConsumable> items)
    {
        foreach (var item in items)
        {
            AddItemToList(item);
        }
    }

    public void AddItemToList(IConsumable item)
    {
        var prefab = PrefabContainer.getConsumableListIconPrefab();
        var image = prefab.GetComponentInChildren<Image>();
        image.sprite = item.Icon;
        var dragHandler = prefab.GetComponent<ListConsumableDragHandler>();
        dragHandler.OnListItemCreated(item);
        prefab.transform.SetParent(ItemList);
    }

    private void FillWeaponList(List<Weapon> weapons)
    {
        foreach (var weapon in weapons)
        {
            AddWeaponToList(weapon);
        }
    }

    public void AddWeaponToList(Weapon weapon)
    {
        var prefab = PrefabContainer.getWeaponListIconPrefab();
        var image = prefab.GetComponentInChildren<Image>();
        image.sprite = weapon.Icon;
        var dragHandler = prefab.GetComponent<ListWeaponDragHandler>();
        dragHandler.OnListItemCreated(weapon);
        prefab.transform.SetParent(WeaponList);
    }
}