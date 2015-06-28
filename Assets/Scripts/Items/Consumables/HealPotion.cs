using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Assets.Scripts.FightCharacters;

namespace Assets.Scripts.Items.Consumables
{
    class HealPotion : Item,IConsumable
    {
        int healAmount;
        public HealPotion(int healAmount)
            : base("Kürbiskernöl", null)
        {
            //this.name = "Kürbiskernöl"
            this.description = string.Format("Heals {0} damage", healAmount);
            this.healAmount = healAmount;
        }

        public bool use(FightCharacter character)
        {
            return character.heal(healAmount);
        }
    }
}
