using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{

    public delegate void UseItemDelegate();
    public delegate void AttackDelegate();

    public class Hero: Elite
    {
        public Hero(List<Viech> activeViecher, Weapon activeWeapon, List<IConsumable> items, int health, int speed, int strength)
        {
            this.activeViecher = activeViecher;
            this.activeWeapon = activeWeapon;
            //this.items = items;
            this.health = health;
            this.maxHealth = health;
            this.speed = speed;
            this.strength = strength;
        }

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
