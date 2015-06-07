using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.Consumables;

namespace Assets.Scripts
{
    delegate void UseItemDelegate();
    delegate void AttackDelegate();

    public class FightPlayer: FightCharacter

    {
        private Weapon activeWeapon;
        private List<FightViech> activeViecher;

        private List<IConsumable> items;

        public FightPlayer(int maxHealth, int speed, int strength, string name, List<FightViech> activeViecher, Weapon activeWeapon, List<Attack> attacks, List<IConsumable> items)
            : base("Player", maxHealth, speed, strength, name, attacks)
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
            List<ButtonDto> buttons = new List<ButtonDto>();


            ButtonDto chooseItemBtn = new ButtonDto(); 
            chooseItemBtn.Name = "";
            chooseItemBtn.Label = "Use Item";
            chooseItemBtn.Callback = chooseItem;

            if(items.Count == 0) {
                chooseItemBtn.Enabled = false;
            }
            else {
                chooseItemBtn.Enabled = true;
            }

            buttons.Add(chooseItemBtn);

            ButtonDto attackBtn = new ButtonDto(); 
            attackBtn.Name = "";
            attackBtn.Label = "Attack";
            attackBtn.Callback = attack;
            attackBtn.Enabled = true;
            buttons.Add(attackBtn);

            FightManager.Instance.showActionMenu(buttons);
        }


        private void chooseItem(string name)
        {
            Debug.Log("Choose Item Action triggerd");

            List<ButtonDto> buttons = new List<ButtonDto>();

            foreach(Item item in items) {
                ButtonDto chooseItemBtn = new ButtonDto();
                chooseItemBtn.Name = item.Name;
                chooseItemBtn.Label = item.Name + " x" + item.Quantity;
                chooseItemBtn.Callback = useItem;
                chooseItemBtn.Enabled = true;
                buttons.Add(chooseItemBtn);
            }

            FightManager.Instance.showSelectionMenu(buttons);
        }

        private void useItem(string name)
        {
            Debug.Log("Use Item Action triggerd");
        }

         private void attack(string name)
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
