﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class FightViech : FightCharacter
    {
        private ElementType type;
        private float catchChance;
        private List<IConsumable> dropItems;

        protected int xpAmount;

        public FightViech(string identifier, int maxHealth, int speed, int strength, List<Attack> attacks, ElementType type, float catchChance, List<IConsumable> dropItems, int xpAmount)
            : base(identifier, maxHealth, speed, strength, attacks)
        {
            this.type = type;
            this.catchChance = catchChance;
            this.dropItems = dropItems;
            this.xpAmount = xpAmount;
        }



        public override void executeTurn()
        {

        }

        protected override void die()
        {

        }

        public void useAttack()
        {

        }

        public void dropItem()
        {

        }

        public void decideJoin()
        {

        }

        public void joinPlayer()
        {

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
    }
}