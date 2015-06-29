using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using System;

using Assets.Scripts;
using Assets.Scripts.Items;
using Assets.Scripts.Items.Consumables;
using Assets.Scripts.Character;

public class PlayerMenueManager : MonoBehaviour 
{
    private static PlayerMenueManager manager;

    private PlayerPanelInteractionManager PlayerPanel;
    private MonsterPanelInteractionManager MonsterPanel;
    private InventoryContentHandler Inventory;
    private Player player;

    void Awake()
    {
        if (manager != null)
            Destroy(this);
        manager = this;

        GameManager.Instance.init();
    }

	// Use this for initialization
	void Start () 
    {
	    PlayerPanel = GetComponentInChildren<PlayerPanelInteractionManager>();
        MonsterPanel = GetComponentsInChildren<MonsterPanelInteractionManager>(true).FirstOrDefault();
        Inventory = GetComponentInChildren<InventoryContentHandler>();
        player = GameManager.Instance.getPlayer();
	}

    public static void SwitchToMonsterPanel()
    {
        manager.PlayerPanel.gameObject.SetActive(false);
        manager.MonsterPanel.gameObject.SetActive(true);
    }

    public static void SwitchToPlayerPanel()
    {
        manager.PlayerPanel.gameObject.SetActive(true);
        manager.MonsterPanel.gameObject.SetActive(false);
    }

    public static void SetMonsterPanelInformation(Viech monster)
    {
        manager.MonsterPanel.ResetMonsterPanel();
		manager.MonsterPanel.SetMonsterInfos(monster);
    }

    public static void SetMonsterFree(Viech monster)
    {
        manager.Inventory.RemoveMonsterFromList(monster);
        manager.player.Viecher.Remove(monster);
        SwitchToPlayerPanel();
    }

    public static void RemoveActiveMonster(Viech monster, int slotNumber)
    {
        Debug.Log("RemoveActiveMonster");

        manager.player.removeActiveViech(slotNumber, monster);
        manager.player.addViech(monster);
        manager.Inventory.AddMonsterToList(monster);
    }

    public static void AddActiveMonster(Viech monster, int slotNumber)
    {
        Debug.Log("AddActiveMonster");

        manager.player.removeViech(monster);
        Debug.Log(slotNumber);
        manager.player.addActiveViech(slotNumber, monster);

        //if (manager.player.ActiveViecher.ContainsKey(slotNumber))
        //{
        //    Debug.Log("ContainsKey");
        //    var prev_monster = manager.player.ActiveViecher[slotNumber];
        //    manager.player.ActiveViecher.Remove(slotNumber);
        //    Debug.Log(prev_monster);
        //    manager.player.Viecher.Add(prev_monster);
        //    manager.Inventory.AddMonsterToList(prev_monster);
        //}

        //else if (monster != null)
        //{
        //    Debug.Log("Add ActiveViech");
        //    manager.player.Viecher.Remove(monster);
        //    manager.player.ActiveViecher.Add(slotNumber, monster);
        //}
    }

    public static void ChangeSlotActiveMonster(int fromSlot, int toSlot, Viech monster)
    {
        Debug.Log("ChangeSlotActiveMonster");

        manager.player.ActiveViecher.Remove(fromSlot);
        manager.player.ActiveViecher.Add(toSlot, monster);
    }

    public static void SetActiveWeapon(Weapon weapon)
    {
        Debug.Log("SetActiveWeapon");
        Debug.Log(weapon);

        manager.player.removeWeapon(weapon);
        manager.player.setActiveWeapon(weapon);
    }

    public static void RemoveActiveWeapon(Weapon weapon)
    {
        Debug.Log("RemoveActiveWeapon");
        Debug.Log(weapon);

        manager.player.addWeapon(weapon);
        manager.Inventory.AddWeaponToList(weapon);
        manager.player.removeActiveWeapon();
    }

    public void BackToMap()
    {
        GameManager.Instance.showWorldMap();
    }
}
