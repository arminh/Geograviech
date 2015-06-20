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
    public Transform ActiveWeapon;
    public Transform ActiveMonster;
	public Player player;

    void Start()
    {
		player = GameManager.Instance.getPlayer();

        ActiveWeapon.GetChild(0);
    }

    public void Consume(IConsumable item)
    {
        throw new System.NotImplementedException();
    }
}
