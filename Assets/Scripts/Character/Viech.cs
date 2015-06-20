using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class Viech : Character
    {
        private ElementType type;

        public Viech(int maxHealth, int speed, int strength, string name, int level, int xp, List<Attack> attacks, ElementType type, GameObject sprite, GameObject icon)
            : base(maxHealth, speed, strength, name, level, xp, attacks, sprite, icon)
        {
            this.type = type;
        }

        public FightViech createFightViech()
        {
            //public FightViech(string identifier, int maxHealth, int speed, int strength, List<Attack> attacks, ElementType type, float catchChance, List<IConsumable> dropItems, int xpAmount)
            return new FightViech(maxHealth, speed, strength, name, attacks, type, -1 , null, -1, sprite, icon);
        }

        public ElementType Type
        {
            get { return type; }
        }
    }
}
