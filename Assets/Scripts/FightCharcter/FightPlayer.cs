using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.Consumables;

namespace Assets.Scripts
{
    delegate void UseItemDelegate();
    delegate void AttackDelegate();

    public class FightPlayer: FightCharacter

    {
        private Weapon activeWeapon;
        private List<FightViech> activeViecher;

        private List<IConsumable> items;

        public FightPlayer(int maxHealth, int speed, int strength, string name, List<FightViech> activeViecher, Weapon activeWeapon, List<Attack> attacks, List<IConsumable> items)
            : base("Player", maxHealth, speed, strength, name, attacks)
        {
            this.activeViecher = activeViecher;
            this.activeWeapon = activeWeapon;
            this.items = items;
        }

        public ItemDto useItem(IConsumable item, FightCharacter character)
        {
            Debug.Log("Use Item Action triggerd");

            item.use(character);
            return null;
        }

        protected override void die()
        {

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
