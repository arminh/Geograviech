using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Assets.Scripts.Effects;
using Assets.Scripts.FightCharacters;

namespace Assets.Scripts.Consumables
{
    class BurnHealer : Item, IConsumable
    {
        public BurnHealer()
        {
            this.name = "Bepanthen Wund und Heilsalbe";
        }
        public bool use(FightCharacter character)
        {
            return character.cureEffect(Effect.EffectType.BURN);
        }
    }
}
