using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class Viech : Character
    {
        private ElementType type;
        private Player owner;

        public Viech(int maxHealth, int currentHealth, int speed, int strength, string name, int level, int xp, List<Attack> attacks, ElementType type, GameObject sprite, Sprite icon)
            : base(maxHealth, currentHealth, speed, strength, name, level, xp, attacks, sprite, icon)
        {
            this.type = type;
        }

        public FightViech createFightViech()
        {
            //public FightViech(string identifier, int maxHealth, int speed, int strength, List<Attack> attacks, ElementType type, float catchChance, List<IConsumable> dropItems, int xpAmount)
            return new FightViech(maxHealth, speed, strength, name, attacks, type, -1 , null, -1, sprite, icon);
        }

        public void setOwner(Player owner)
        {
            this.owner = owner;
        }

        protected override void levelUp()
        {
            if (level + 1 <= owner.Level)
            {
                level += 1;
            }
        }

        public ElementType Type
        {
            get { return type; }
        }
    }
}
