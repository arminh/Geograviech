using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Assets.Scripts.Effects;
using Assets.Scripts.FightCharacters;

namespace Assets.Scripts.Items.Consumables
{
    class Antidote : Item ,IConsumable
    {
        public Antidote()
            : base("Schnaps")
        {
            this.icon = GameManager.Instance.Icons["schnaps"];
        }
        public bool use(FightCharacter character)
        {
            return character.cureEffect(Effect.EffectType.POISON);
        }
    }
}
