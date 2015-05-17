using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Consumables
{
    class Awakener : Item, IConsumable
    {
        public Awakener()
        {
            this.name = "Kaffee";
        }
        public bool use(FightCharacter character)
        {
            return character.cureEffect(Effect.EffectType.SLEEP);
        }
    }
}
