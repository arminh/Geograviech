using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Assets.Scripts.Effects;
using Assets.Scripts.FightCharacters;

namespace Assets.Scripts.Consumables
{
    class StunnHealer : Item, IConsumable
    {
        public StunnHealer()
        {
            this.name = "Eiseimer";
        }
        public bool use(FightCharacter character)
        {
            return character.cureEffect(Effect.EffectType.STUN);
        }
    }
}
