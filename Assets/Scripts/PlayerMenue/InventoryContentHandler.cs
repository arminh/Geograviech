using UnityEngine;
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
        var listItem = Instantiate(prefab);
        var image = listItem.GetComponentInChildren<Image>();
        image.sprite = monster.Icon;
        var dragHandler = listItem.GetComponent<ListMonsterDragHandler>();
        dragHandler.OnListItemCreated(monster);
        listItem.transform.SetParent(MonsterList);
    }

    public void RemoveMonsterFromList(Viech monster)
    {
        ListMonsterDragHandler monsterItem = null;
        foreach (var item in MonsterList.GetComponentsInChildren<ListMonsterDragHandler>())
        {
            if(item.Item.Equals(monster))
            {
                monsterItem = item;
            }
        }
        Destroy(monsterItem.gameObject);
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
        var listItem = Instantiate(prefab);
        var image = listItem.GetComponentInChildren<Image>();
        image.sprite = item.Icon;
        var dragHandler = listItem.GetComponent<ListConsumableDragHandler>();
        dragHandler.OnListItemCreated(item);
        listItem.transform.SetParent(ItemList);
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
        var listItem = Instantiate(prefab);
        var image = listItem.GetComponentInChildren<Image>();
        image.sprite = weapon.Icon;
        var dragHandler = listItem.GetComponent<ListWeaponDragHandler>();
        dragHandler.OnListItemCreated(weapon);
        listItem.transform.SetParent(WeaponList);
    }
}
