using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class FightBoss : FightViech
    {
       private List<FightViech> activeViecher;

       public FightBoss(string identifier, int maxHealth, int speed, int strength, List<Attack> attacks, ElementType type, float catchChance, List<IConsumable> dropItems, int xpAmount, List<FightViech> activeViecher)
            : base(identifier, maxHealth, speed, strength, attacks, type, catchChance, dropItems, xpAmount)
        {
            this.activeViecher = activeViecher;
        }

        public override void executeTurn()
        {
            throw new NotImplementedException();
        }

        protected override void die()
        {
            throw new NotImplementedException();
        }

        public List<FightViech> ActiveViecher
        {
            get { return activeViecher; }
        }
    }
}
