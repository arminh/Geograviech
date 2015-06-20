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
        private const int num_drops = 3;

        private ElementType type;
        private int catchChance;
        private List<Item> drops;

        protected int xpAmount;
        protected int level;

        protected System.Random rand;

        public FightViech(int maxHealth, int speed, int strength, string name, List<Attack> attacks, ElementType type, int catchChance, List<Item> drops, int xpAmount, GameObject sprite, Sprite icon)
            : base(maxHealth, speed, strength, name, attacks, sprite, icon)
        {
            this.type = type;
            this.catchChance = catchChance;
            this.drops = drops;
            this.xpAmount = xpAmount;

            rand = new System.Random();
        }

        public List<Item> dropItems()
        {
            List<Item> droppedItems = new List<Item>();

            for (int i = 0; i < num_drops; i++)
            {
                Item dropItem = drops[rand.Next(0, drops.Count - 1)];

                if (rand.Next(1, 100) <= dropItem.DropChance)
                {
                    droppedItems.Add(dropItem);
                }
            }

            return droppedItems;
        }

        public bool decideJoin()
        {
            if (rand.Next(1, 100) <= catchChance)
            {
                return true;
            }

            return false;
        }

        public ElementType Type
        {
            get { return type; }
        }


        public float CatchChance
        {
            get { return catchChance; }
        }


        public List<Item> Drops
        {
            get { return drops; }
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
