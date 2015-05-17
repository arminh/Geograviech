using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class Viech : Character
    {
        private ElementType type;

        public Viech(int maxHealth, int speed, int strength, string name, string identifier, int level, int xp, List<Attack> attacks, ElementType type)
            : base(maxHealth, speed, strength, name, identifier, level, xp, attacks)
        {
            this.type = type;
        }

        public FightViech createFightViech()
        {
            //public FightViech(string identifier, int maxHealth, int speed, int strength, List<Attack> attacks, ElementType type, float catchChance, List<IConsumable> dropItems, int xpAmount)
            return new FightViech(identifier, maxHealth, speed, strength, attacks, type, -1 , null, -1);
        }

        public ElementType Type
        {
            get { return type; }
        }
    }
}
