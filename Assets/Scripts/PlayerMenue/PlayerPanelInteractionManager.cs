using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

using Assets.Scripts;
using Assets.Scripts.PlayerMenue;
using Assets.Scripts.Items.Consumables;
using Assets.Scripts.Character;
using Assets.Scripts.Items;

public class PlayerPanelInteractionManager : MonoBehaviour, IConsumableInteraction
{
    public Transform ActiveWeaponSlot;
    public List<Transform> ActiveMonsterSlots;
	public Player player;
    public HoldWeapon WeaponArm;

    void Start()
    {
		player = GameManager.Instance.getPlayer();
        if (player.ActiveWeapon != null)
        {
            var weaponPrefab = PrefabContainer.getWeaponIconPrefab();
            var weaponItem = Instantiate(weaponPrefab);
            var image = weaponItem.GetComponentInChildren<Image>();
            image.sprite = player.ActiveWeapon.Icon;
            var itemHandl = weaponItem.GetComponent<DragItemHandler>();
            itemHandl.Item = player.ActiveWeapon;
            weaponItem.transform.SetParent(ActiveWeaponSlot);
            weaponItem.transform.localScale = new Vector3(1, 1, 1);
        }

        var monsterPrefab = PrefabContainer.getMonsterIconPrefab();
        for (int index = 0; index < ActiveMonsterSlots.Count; index++)
        {
            if (player.ActiveViecher.ContainsKey(index))
            {
                var monster = player.ActiveViecher[index];
                var monsterItem = Instantiate(monsterPrefab);
                var monsterImage = monsterItem.GetComponentInChildren<Image>();
                monsterImage.sprite = monster.Icon;
                var itemHandl = monsterItem.GetComponent<DragItemHandler>();
                itemHandl.Item = monster;
                monsterItem.transform.SetParent(ActiveMonsterSlots.ElementAt(index));
                monsterItem.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        WeaponArm = GetComponentInChildren<HoldWeapon>();
    }

    public void RemoveWeaponFromHand()
    {
        WeaponArm.RemoveWeapon();
    }

    public void AddWeaponToHand(Weapon weapon)
    {
        WeaponArm.AddWeapon(weapon.Icon);
    }

    public void Consume(IConsumable item)
    {
        throw new System.NotImplementedException();
    }
}
