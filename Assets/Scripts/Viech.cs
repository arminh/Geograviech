using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Viech: Character
    {
        private ElementType type;
        private float catchChance;
        private List<Item> dropItems;

        protected int xp;
        protected int levelUpXp;

        protected int GainXp { get; private set; }


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

        public override bool isElite()
        {
            return false;
        }
    }
}
