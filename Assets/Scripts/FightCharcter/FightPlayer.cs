﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.Items;
using Assets.Scripts.Items.Consumables;

namespace Assets.Scripts.FightCharacters
{
    delegate void UseItemDelegate();
    delegate void AttackDelegate();

    public class FightPlayer: FightCharacter

    {
        private Weapon activeWeapon;
        private List<FightViech> activeViecher;

        private List<IConsumable> items;

        public FightPlayer(int maxHealth, int speed, int strength, string name, List<FightViech> activeViecher, Weapon activeWeapon, List<Attack> attacks, List<IConsumable> items, string prefabId, string iconId)
            : base(maxHealth, speed, strength, name, attacks, prefabId, iconId)
        {
            this.activeViecher = activeViecher;

            if (activeWeapon == null)
            {
                ItemFactory fact = new ItemFactory();
                this.activeWeapon = fact.getFistWeapon();
            }
            else
            {
                this.activeWeapon = activeWeapon;
            }

            this.items = items;
        }

        public void useItem(IConsumable item, FightCharacter character)
        {
            Debug.Log("Use Item Action triggerd");

            item.use(character);
        }

        public void useConsumable(IConsumable consumable)
        {
            consumable.use(this);
        }

        public Weapon ActiveWeapon
        {
            get { return activeWeapon; }
        }

        public List<FightViech> ActiveViecher
        {
            get { return activeViecher; }
        }

        public List<IConsumable> Items
        {
            get { return items; }
        }

    }
}
