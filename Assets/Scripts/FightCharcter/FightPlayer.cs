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

        private List<IConsumable> items;

        public FightPlayer(int maxHealth, int speed, int strength, List<FightViech> activeViecher, Weapon activeWeapon, List<Attack> attacks, List<IConsumable> items)
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
            Debug.Log("PLayer.showChooseActionGui");
            Dictionary<string, Action> dict = new Dictionary<string, Action>();

            Action chooseItemAction = chooseItem;
            dict.Add("Use Item", chooseItemAction);

            Action attackAction = attack;
            dict.Add("Attack", attackAction);
            

            FightManager.Instance.showActionMenu(dict);
        }


        private void chooseItem()
        {
            Debug.Log("Choose Item Action triggerd");

            Action<string> useItemAction = useItem;

            Dictionary<string, int> dict = new Dictionary<string, int>();

            foreach(Item item in items) {
                dict.Add(item.Name, item.Quantity);
            }

            FightManager.Instance.showSelectionMenu(useItemAction, dict);
        }

        private void useItem(string name)
        {
            Debug.Log("Use Item Action triggerd");
        }

         private void attack()
        {
            Debug.Log("Attack Action triggerd");
            FightManager.Instance.attackEnemy(activeWeapon.Attack);
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

        public List<IConsumable> Items
        {
            get { return items; }
        }
    }
}
