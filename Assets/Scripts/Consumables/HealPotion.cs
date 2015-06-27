using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Assets.Scripts.FightCharacters;

namespace Assets.Scripts.Consumables
{
    class HealPotion : Item,IConsumable
    {
        int healAmount;
        public HealPotion(string name, int healAmount, int dropChance, int quantity, Sprite icon) 
            : base(name, dropChance, quantity, icon)
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
