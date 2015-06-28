using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Assets.Scripts.FightCharacters;

namespace Assets.Scripts.Items.Consumables
{
    class MajorHealPotion : Item , IConsumable
    {
        int healAmount;
        public MajorHealPotion(int healAmount)
            : base("Lebertran")
        {
            //this.name = "Kürbiskernöl"
            this.icon = GameManager.Instance.Icons["lebertran"];
            this.description = string.Format("Heals {0} damage", healAmount);
            this.healAmount = healAmount;
        }

        public bool use(FightCharacter character)
        {
            return character.heal(healAmount);
        }
    }
}
