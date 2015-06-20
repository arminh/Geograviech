using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;
using Assets.Scripts.Consumables;
using UnityEngine.UI;
using System.Linq;

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
        MonsterPanel = GetComponentInChildren<MonsterPanelInteractionManager>();
        Inventory = GetComponentInChildren<InventoryContentHandler>();
        player = GameManager.Instance.getPlayer();
	}

    public static void SwitchMonsterPlayerPanel()
    {
        if (manager.MonsterPanel.gameObject.activeSelf)
        {
            manager.MonsterPanel.ResetMonsterPanel();
        }
        manager.PlayerPanel.gameObject.SetActive(!manager.PlayerPanel.gameObject.activeSelf);
        manager.MonsterPanel.gameObject.SetActive(!manager.MonsterPanel.gameObject.activeSelf);
    }

    public static void SetMonsterPanelInformation(Viech monster)
    {
		manager.MonsterPanel.SetMonsterInfos(monster);
    }

    public static void SetMonsterFree(Viech monster)
    {
        manager.Inventory.RemoveMonsterFromList(monster);
        manager.player.Viecher.Remove(monster);
    }

    public static void SwapActiveMonster(Viech monster, int slotNumber)
    {
        manager.player.Viecher.Remove(monster);
        var prev_monster = manager.player.ActiveViecher.ElementAt(slotNumber);
        if (prev_monster != null)
        {
            manager.player.Viecher.Add(prev_monster);
            manager.Inventory.AddMonsterToList(prev_monster);
        }
        manager.player.ActiveViecher.Insert(slotNumber, monster);
    }

    public static void SwapActiveWeapon(Weapon weapon)
    {
        manager.player.Weapons.Remove(weapon);
        if (manager.player.ActiveWeapon != null)
        {
            manager.player.Weapons.Add(manager.player.ActiveWeapon);
            manager.Inventory.AddWeaponToList(manager.player.ActiveWeapon);
        }
        manager.player.ActiveWeapon = weapon;
    }
}
