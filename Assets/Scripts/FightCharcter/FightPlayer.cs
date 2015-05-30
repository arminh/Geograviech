using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    delegate void UseItemDelegate();
    delegate void AttackDelegate();

    public class FightPlayer: FightCharacter

    {
        private Weapon activeWeapon;
        private List<FightViech> activeViecher;

        private List<IItem> items;

        public FightPlayer(int maxHealth, int speed, int strength, List<FightViech> activeViecher, Weapon activeWeapon, List<Attack> attacks, List<IItem> items)
            : base("Player", maxHealth, speed, strength, attacks)
        {
            this.activeViecher = activeViecher;
            this.activeWeapon = activeWeapon;
            this.items = items;
        }

        public override void executeTurn()
        {
            showChooseActionGui();
        }



        public void showChooseActionGui()
        {
            List<string> labels = new List<string>();
            List<Action> actions = new List<Action>();
            
            Action useItemAction = useItem;
            labels.Add("Use Item");
            actions.Add(useItemAction);

            Action attackAction = attack;
            labels.Add("Attack");
            actions.Add(attackAction);
            

            FightManager.Instance.showMenu(labels, actions);
        }


        private static void useItem()
        {
            Debug.Log("Use Item Action triggerd");
        }

         private void attack()
        {
            Debug.Log("Attack Action triggerd");
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

        public Weapon ActiveWeapon
        {
            get { return activeWeapon; }
        }

        public List<FightViech> ActiveViecher
        {
            get { return activeViecher; }
        }

        public List<IItem> Items
        {
            get { return items; }
        }
    }
}
