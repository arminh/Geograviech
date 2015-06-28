using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Assets.Scripts.Effects;
using Assets.Scripts.FightCharacters;

namespace Assets.Scripts.Items.Consumables
{
    class StunHealer : Item, IConsumable
    {
        public StunHealer()
            : base("Eiseimer")
        {
            //this.icon = GameManager.Instance.Icons[""];
        }
        public bool use(FightCharacter character)
        {
            return character.cureEffect(Effect.EffectType.STUN);
        }
    }
}
