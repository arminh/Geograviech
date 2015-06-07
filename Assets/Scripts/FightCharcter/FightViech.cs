using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.Consumables;

namespace Assets.Scripts
{
    public class FightViech : FightCharacter
    {
        private ElementType type;
        private float catchChance;
        private List<IConsumable> dropItems;

        protected int xpAmount;
        protected int level;

        public FightViech(string identifier, int maxHealth, int speed, int strength, string name, List<Attack> attacks, ElementType type, float catchChance, List<IConsumable> dropItems, int xpAmount)
            : base(identifier, maxHealth, speed, strength, name, attacks)
        {
            this.type = type;
            this.catchChance = catchChance;
            this.dropItems = dropItems;
            this.xpAmount = xpAmount;
        }

        protected override void die()
        {

        }

        public void dropItem()
        {

        }

        public bool decideJoin()
        {
            return true;
        }

        public ElementType Type
        {
            get { return type; }
        }


        public float CatchChance
        {
            get { return catchChance; }
        }


        public List<IConsumable> DropItems
        {
            get { return dropItems; }
        }


        public int XpAmount
        {
            get { return xpAmount; }
        }

        public int Level
        {
            get { return level; }
        }

    }
}
