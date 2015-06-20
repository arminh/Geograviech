using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;
using Assets.Scripts.Consumables;

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

    public static void OnWeaponAdded(Weapon weapon)
    {
        PlayerMenueManager manager = FindObjectOfType<PlayerMenueManager>();
        if (manager)
        {
            manager.SetActiveWeapon(weapon);
        }
    }

    public static void OnWeaponRemoved(Weapon weapon)
    {
        PlayerMenueManager manager = FindObjectOfType<PlayerMenueManager>();
        if (manager)
        {
            manager.RemoveActiveWeapon(weapon);
        }
    }

    public static void OnMonsterAdded(Viech monster)
    {
        PlayerMenueManager manager = FindObjectOfType<PlayerMenueManager>();
        if (manager)
        {
            manager.AddMonsterToActive(monster);
        }
    }

    public static void OnMonsterRemoved(Viech monster)
    {
        PlayerMenueManager manager = FindObjectOfType<PlayerMenueManager>();
        if (manager)
        {
            manager.RemoveMonsterFromActive(monster);
        }
    }
	
    public void SwitchMonsterPlayerPanel()
    {
        if (MonsterPanel.gameObject.activeSelf)
        {
            MonsterPanel.ResetMonsterPanel();
        }
        PlayerPanel.gameObject.SetActive(!PlayerPanel.gameObject.activeSelf);
        MonsterPanel.gameObject.SetActive(!MonsterPanel.gameObject.activeSelf);
    }

    public void SetMonsterPanelInformation(Viech monster)
    {
		MonsterPanel.SetMonsterInfos(monster);
    }

    public void SetMonsterFree(Viech monster)
    {
		player.Viecher.Remove(monster);
    }

    public void AddMonsterToActive(Viech monster)
    {
		player.Viecher.Remove(monster);
        player.ActiveViecher.Add(monster);
    }

    public void RemoveMonsterFromActive(Viech monster)
    {
        player.ActiveViecher.Remove(monster);
		player.Viecher.Add(monster);
    }

    public void SetActiveWeapon(Weapon weapon)
    {
		player.Weapons.Remove (weapon);
        player.ActiveWeapon = weapon;
    }

	public void RemoveActiveWeapon(Weapon weapon)
    {
        player.ActiveWeapon = null;
		player.Weapons.Add (weapon);
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
