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
        private Weapon activeWeapon;

        private List<IConsumable> consumables;

        public Hero(List<Viech> activeViecher, Weapon activeWeapon, List<IConsumable> consumables, int health, int speed, int strength)
        {
            this.activeViecher = activeViecher;
            this.activeWeapon = activeWeapon;
            this.consumables = consumables;
            this.health = health;
            this.maxHealth = health;
            this.speed = speed;
            this.strength = strength;
        }



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


        public void useConsumable(IConsumable consumable)
        {
            consumable.use(this);
        }

        public void useWeapon()
        {

        }
    }
}
