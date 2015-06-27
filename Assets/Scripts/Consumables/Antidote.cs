using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Assets.Scripts.Effects;
using Assets.Scripts.FightCharacters;

namespace Assets.Scripts.Consumables
{
    class Antidote : Item,IConsumable
    {
        public Antidote()
        {
            this.name = "Pfefferminz-Tee";
        }
        public bool use(FightCharacter character)
        {
            return character.cureEffect(Effect.EffectType.POISON);
        }
    }
}
