using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class FightViech : FightCharacter
    {
        private ElementType type;
        private float catchChance;
        private List<IConsumable> dropItems;

        protected int xpAmount;
        protected int level;

        public FightViech(string identifier, int maxHealth, int speed, int strength, List<Attack> attacks, ElementType type, float catchChance, List<IConsumable> dropItems, int xpAmount)
            : base(identifier, maxHealth, speed, strength, attacks)
        {
            this.type = type;
            this.catchChance = catchChance;
            this.dropItems = dropItems;
            this.xpAmount = xpAmount;
        }

        public override void executeTurn()
        {

            Action<string> useAttackAction = useAttack;

            Dictionary<string, int> dict = new Dictionary<string, int>();

            foreach (Attack att in attacks)
            {
                dict.Add(att.Name, att.Damage);
            }

            FightManager.Instance.showItemMenu(useAttackAction, dict);
        }

        protected override void die()
        {

        }

        private void useAttack(string name)
        {
            Debug.Log("Use Attack Action triggerd");

            foreach(Attack att in attacks) {
                if (att.Name == name)
                {
                    FightManager.Instance.attackEnemy(att);
                
                }
            }
        }

        public void dropItem()
        {

        }

        public bool decideJoin()
        {
            return true;
        }

        public ElementType Type
        {
            get { return type; }
        }


        public float CatchChance
        {
            get { return catchChance; }
        }


        public List<IConsumable> DropItems
        {
            get { return dropItems; }
        }


        public int XpAmount
        {
            get { return xpAmount; }
        }

        public int Level
        {
            get { return level; }
        }

    }
}
