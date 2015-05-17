using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class Player : Character
    {
        private List<Viech> viecher;
        private List<Viech> activeViecher;
        
        private List<Weapon> weapons;
        private Weapon activeWeapon;

        private List<IItem> items;

        public Player(int maxHealth, int speed, int strength, string name, string identifier, int xp, int level, List<Viech> viecher, List<Viech> activeViecher, List<Weapon> weapons, Weapon activeWeapon, List<IItem> items, List<Attack> attacks): base(maxHealth, speed, strength, name, identifier, level, xp, attacks)
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

            foreach (Viech viech in activeViecher)
            {
                fightViecher.Add(viech.createFightViech());
            }


            return new FightPlayer(maxHealth, speed, strength, fightViecher, activeWeapon, attacks, items);
        }

        public void gainXp(int amount)
        {

        }

        public List<Viech> Viecher
        {
            get { return viecher; }
        }

        public List<Viech> ActiveViecher
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
        }
        public List<IItem> Items
        {
            get { return items; }
        }
    }
}
