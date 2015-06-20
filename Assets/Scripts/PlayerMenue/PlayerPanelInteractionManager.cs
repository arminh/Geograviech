using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using Assets.Scripts.PlayerMenue;
using Assets.Scripts.Consumables;

public class PlayerPanelInteractionManager : MonoBehaviour, IConsumableInteraction
{
    public Transform ActiveWeaponSlot;
    public List<Transform> ActiveMonsterSlots;
	public Player player;

    void Start()
    {
		player = GameManager.Instance.getPlayer();

        var prefab = PrefabContainer.getWeaponIconPrefab();
        var weaponItem = Instantiate(prefab);
        var image = weaponItem.GetComponentInChildren<Image>();
        image.sprite = player.ActiveWeapon.Icon;
        weaponItem.transform.SetParent(ActiveWeaponSlot);

        prefab = PrefabContainer.getMonsterIconPrefab();
        for (int index = 0; index < ActiveMonsterSlots.Count; index++)
        {
            var monster = player.ActiveViecher.ElementAt(index);
            if (monster != null)
            {
                var monsterItem = Instantiate(prefab);
                var monsterImage = monsterItem.GetComponentInChildren<Image>();
                monsterImage.sprite = monster.Icon;
                var itemHandl = monsterItem.GetComponent<DragItemHandler>();
                itemHandl.Item = monster;
                weaponItem.transform.SetParent(ActiveMonsterSlots.ElementAt(index));
            }
        }
    }

    public void Consume(IConsumable item)
    {
        throw new System.NotImplementedException();
    }
}
