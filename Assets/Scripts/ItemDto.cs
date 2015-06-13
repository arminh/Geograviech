using Assets.Scripts.Consumables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class ItemDto
    {
        private IConsumable item;
        private FightCharacter character;
        private int healedAmount;
        private Effect curedEffect;


        public IConsumable Item
        {
            get { return item; }
        }

        public FightCharacter Character
        {
            get { return character; }
        }

        public int HealedAmount
        {
            get { return healedAmount; }
        }

        public Effect CuredEffect
        {
            get { return curedEffect; }
        }

        
    }
}
