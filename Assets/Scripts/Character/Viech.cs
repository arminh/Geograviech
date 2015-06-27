using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Assets.Scripts.Utils;
using Assets.Scripts.FightCharacters;

namespace Assets.Scripts.Character
{
    public class Viech : Character
    {
        private Enums.ElementType type;
        private Player owner;

        public Viech(int maxHealth, int currentHealth, int speed, int strength, string name, int level, int xp, List<Attack> attacks, Enums.ElementType type, GameObject sprite, Sprite icon)
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

        public Enums.ElementType Type
        {
            get { return type; }
        }
    }
}
