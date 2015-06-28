using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Assets.Scripts.Utils;
using Assets.Scripts.Items;
using Assets.Scripts.Items.Consumables;

namespace Assets.Scripts.FightCharacters
{
    public class FightViech : FightCharacter
    {
        private const int num_drops = 3;

        private Enums.ElementType type;
        private int catchChance;
        private List<Item> drops;

        protected int xpAmount;
        protected int level;

        protected System.Random rand;

        public FightViech(int maxHealth, int speed, int strength, string name, List<Attack> attacks, Enums.ElementType type, int catchChance, List<Item> drops, int xpAmount, string prefabId, string iconId)
            : base(maxHealth, speed, strength, name, attacks, prefabId, iconId)
        {
            this.type = type;
            this.catchChance = catchChance;
            this.drops = drops;
            this.xpAmount = xpAmount;

            rand = new System.Random();
        }

        public List<Item> dropItems()
        {
            ItemFactory fact = new ItemFactory();

            List<Item> droppedItems = fact.createRandomDrops(level, 4);

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

        public Enums.ElementType Type
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
