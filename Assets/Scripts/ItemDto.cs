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
        private FightViech viech;
        private int healedAmount;
        private Effect curedEffect;


        public IConsumable Item
        {
            get { return item; }
        }

        public FightViech Viech
        {
            get { return viech; }
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
