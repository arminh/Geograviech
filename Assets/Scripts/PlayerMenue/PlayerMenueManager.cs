using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;
using Assets.Scripts.Consumables;
using UnityEngine.UI;

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
    }

	// Use this for initialization
	void Start () 
    {
	    PlayerPanel = GetComponentInChildren<PlayerPanelInteractionManager>();
        MonsterPanel = GetComponentInChildren<MonsterPanelInteractionManager>();
        Inventory = GetComponentInChildren<InventoryContentHandler>();
        player = GameManager.Instance.getPlayer();
	}

    //    public static void OnWeaponAdded(Weapon weapon)
    //{
    //    manager.SetActiveWeapon(weapon);
    //}

    //public static void OnWeaponRemoved(Weapon weapon)
    //{
    //    manager.RemoveActiveWeapon(weapon);
    //}

    //public static void OnMonsterAdded(Viech monster)
    //{
    //    manager.AddMonsterToActive(monster);
    //}

    //public static void OnMonsterRemoved(Viech monster)
    //{

    //        manager.RemoveMonsterFromActive(monster);
    //    }
    //}

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
        manager.player.Viecher.Remove(monster);
    }

    public static void AddMonsterToActive(Viech monster)
    {
        manager.player.Viecher.Remove(monster);
        manager.player.ActiveViecher.Add(monster);
    }

    public static void RemoveMonsterFromActive(Viech monster)
    {
        manager.player.ActiveViecher.Remove(monster);
        manager.player.Viecher.Add(monster);
    }

    public static void SetActiveWeapon(Weapon weapon)
    {
        manager.player.Weapons.Remove(weapon);
        manager.player.ActiveWeapon = weapon;
    }

	public static void RemoveActiveWeapon(Weapon weapon)
    {
        manager.player.ActiveWeapon = null;
        manager.player.Weapons.Add(weapon);
    }

    public List<Viech> GetAllMonstersOfPlayer()
    {
        return player.Viecher;
    }

    public List<IConsumable> GetAllConsumablesOfPlayer()
    {
        return player.Items;
    }

    public List<Weapon> GetAllWeaponsOfPlayer()
    {
        return player.Weapons;
    }
}
