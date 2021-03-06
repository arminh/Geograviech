﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Assets.Scripts.Items;
using Assets.Scripts.Items.Consumables;
using Assets.Scripts.FightCharacters;

namespace Assets.Scripts.Character
{
    public class Player : Character
    {
        private List<Viech> viecher;
        private Dictionary<int, Viech> activeViecher;
        
        private List<Weapon> weapons;
        private Weapon activeWeapon;

        private List<IConsumable> items;

        public Player(int maxHealth, int currentHealth, int speed, int strength, string name, int xp, int level, List<Viech> viecher, Dictionary<int, Viech> activeViecher, List<Weapon> weapons, Weapon activeWeapon, List<IConsumable> items, List<Attack> attacks, string prefabId, string iconId)
            : base(maxHealth, currentHealth, speed, strength, name, level, xp, attacks, prefabId, iconId)
        {
           
            this.viecher = viecher;
            this.activeViecher = activeViecher;
            this.weapons = weapons;
            this.activeWeapon = activeWeapon;
            this.items = items;
        }

        public FightPlayer createHero()
        {
            // public FightPlayer(int maxHealth, int speed, int strength, List<FightViech> activeViecher, Weapon activeWeapon, List<Attack> attacks, List<IItem> items)
            List<FightViech> fightViecher = new List<FightViech>();

            var query = from slot in activeViecher select slot.Value;
            foreach (Viech viech in query)
            {
				if(viech != null)
				{
                	fightViecher.Add(viech.createFightViech());
				}
            }


            return new FightPlayer(maxHealth, speed, strength, name, fightViecher, activeWeapon, attacks, items, prefabId, iconId);
        }

        protected override void levelUp()
        {

        }

        public void addViech(Viech viech)
        {
            viecher.Add(viech);
            viech.setOwner(this);
        }

        public void removeViech(Viech viech)
        {
            viecher.Remove(viech);
        }

        public void addActiveViech(int slot, Viech viech)
        {
            if (!activeViecher.ContainsKey(slot))
            {
                activeViecher.Add(slot, viech);
            }
            else
            {
                activeViecher[slot] = viech;
            }
            viech.setOwner(this);
        }

        public void removeActiveViech(int slot, Viech viech)
        {
            if(activeViecher.ContainsKey(slot))
                activeViecher.Remove(slot);
        }

        public void addWeapon(Weapon weapon)
        {
            weapons.Add(weapon);
        }

        public void removeWeapon(Weapon weapon)
        {
            weapons.Remove(weapon);
        }

        public void setActiveWeapon(Weapon weapon)
        {
            activeWeapon = weapon;
        }
        
        public void removeActiveWeapon()
        {
            activeWeapon = null;
        }

        
        public void addConsumable(Item consumable)
        {
            foreach (Item item in items)
            {
                if(consumable.Name == item.Name)
                {
                    item.Quantity += 1;
                    return;
                }
            }
            items.Add((IConsumable)consumable);
        }

        public void removeConsumable(Item item, int amount)
        {
            if(item.Quantity > amount) 
            {
                item.Quantity -= amount;
            }
            else
            {
                items.Remove((IConsumable)item);
            }
        }

        public List<Viech> Viecher
        {
            get { return viecher; }
        }

        public Dictionary<int, Viech> ActiveViecher
        {
            get { return activeViecher; }
        }
        public List<Weapon> Weapons
        {
            get { return weapons; }
        }
        public Weapon ActiveWeapon
        {
            get { return activeWeapon; }
            set { activeWeapon = value; }
        }
        public List<IConsumable> Items
        {
            get { return items; }
        }
    }
}
