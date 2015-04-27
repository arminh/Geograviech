using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{

    public delegate void UseItemDelegate();
    public delegate void AttackDelegate();

    class Hero: Elite
    {
        private List<Weapon> weapons;
        private Weapon activeWeapon;

        private List<IConsumable> consumables;

        public override void executeTurn()
        {
            //FightManager.instance().showChooseActionGui(useItem);
            showChooseActionGui(useItem, attack);

        }

        public void showChooseActionGui(UseItemDelegate useItem, AttackDelegate attack)
        {

        }

        private void useItem()
        {

        }

         private void attack()
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
