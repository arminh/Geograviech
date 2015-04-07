using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Consumables
{
    class FreezeHealer : Item, IConsumable
    {
        public FreezeHealer()
        {
            this.name = "Heizdecke";
        }
        public void use(Character character)
        {
            character.cureEffect(Effect.EffectType.FREEZE);
        }
    }
}
