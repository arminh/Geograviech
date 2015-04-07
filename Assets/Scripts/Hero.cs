using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class Hero: Elite
    {
        private List<Weapon> weapons;
        private Weapon activeWeapon;

        private List<IConsumable> consumables;

        public override void executeTurn()
        {

        }

        protected override void die()
        {

        }

        public override void levelUp()
        {

        }

        protected override void chooseViech()
        {

        }

        protected override void switchViech()
        {

        }

        public void useConsumable(IConsumable consumable)
        {
            consumable.use(this);
        }

        public void useWeapon()
        {

        }

        public void chooseWeapon()
        {

        }
    }
}
