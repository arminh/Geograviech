using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class Boss: Elite
    {
        private ElementType type;
        private float catchChance;
        private List<Item> dropItems;

        protected int xp;
        protected int levelUpXp;

        protected int GainXp {get; private set; }


        public override void executeTurn()
        {
            throw new NotImplementedException();
        }

        protected override void die()
        {
            throw new NotImplementedException();
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
    }
}
