using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class Reviver : Item,IConsumable
    {
        int healAmount;
        public Reviver(int healAmount)
        {
            this.name = "Defibrillator";
            this.healAmount = healAmount;
        }

        public bool use(Character character)
        {
            return character.revive(healAmount);
        }
    }
}
