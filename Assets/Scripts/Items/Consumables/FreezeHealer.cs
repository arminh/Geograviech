using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Assets.Scripts.Effects;
using Assets.Scripts.FightCharacters;

namespace Assets.Scripts.Items.Consumables
{
    class FreezeHealer : Item, IConsumable
    {
        public FreezeHealer()
            : base("Wärmeflasche")
        {
            this.icon = GameManager.Instance.Icons["waermeflasche"];
            this.description = "Warms up.";
        }
        public bool use(FightCharacter character)
        {
            return character.cureEffect(Effect.EffectType.FREEZE);
        }
    }
}
