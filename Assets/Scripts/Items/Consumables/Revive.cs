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
            : base("Defibrillator")
        {
            this.healAmount = healAmount;
            this.icon = GameManager.Instance.Icons["revive1"];
            this.description = string.Format("Revives and heals {0} damage.", healAmount);
        }

        public bool use(FightCharacter character)
        {
            return character.revive(healAmount);
        }
    }
}
