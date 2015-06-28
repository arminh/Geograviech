using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Assets.Scripts.FightCharacters;

namespace Assets.Scripts.Items.Consumables
{
    class Reviver : Item,IConsumable
    {
        int healAmount;
        public Reviver(int healAmount)
            : base("Defibrillator", null)
        {
            this.healAmount = healAmount;
        }

        public bool use(FightCharacter character)
        {
            return character.revive(healAmount);
        }
    }
}
