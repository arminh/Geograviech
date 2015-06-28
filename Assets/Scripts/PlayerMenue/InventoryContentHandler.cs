using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

using Assets.Scripts;
using Assets.Scripts.Items;
using Assets.Scripts.Items.Consumables;
using Assets.Scripts.Character;


public class InventoryContentHandler : MonoBehaviour 
{
    public Transform MonsterList;
    public Transform ItemList;
    public Transform WeaponList;

	// Use this for initialization
	void Start () 
    {
        var player = GameManager.Instance.getPlayer();
        FillMonsterList(player.Viecher);
        FillItemList(player.Items);
        FillWeaponList(player.Weapons);      
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
        var dragHandler = listItem.GetComponent<ListMonsterDragHandler>();
        dragHandler.OnItemCreated(monster);
        listItem.transform.SetParent(MonsterList);
        listItem.transform.localScale = new Vector3(1, 1, 1);
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
        var dragHandler = listItem.GetComponent<ListConsumableDragHandler>();
        dragHandler.OnItemCreated(item);
        listItem.transform.SetParent(ItemList);
        listItem.transform.localScale = new Vector3(1, 1, 1);
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
        var dragHandler = listItem.GetComponent<ListWeaponDragHandler>();
        dragHandler.OnItemCreated(weapon);
        listItem.transform.SetParent(WeaponList);
        listItem.transform.localScale = new Vector3(1, 1, 1);
    }
}
