using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class HealPotion : Item,IConsumable
    {
        int healAmount;
        public HealPotion(int healAmount)
        {
            this.name = "Kürbiskernöl";
            this.healAmount = healAmount;
        }

        public void use(Character character)
        {
            character.heal(healAmount);
        }
    }
}
