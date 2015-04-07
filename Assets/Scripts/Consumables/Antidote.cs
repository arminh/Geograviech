using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Consumables
{
    class Antidote : Item,IConsumable
    {
        public Antidote()
        {
            this.name = "Pfefferminz-Tee";
        }
        public void use(Character character)
        {
            character.cureEffect(Effect.EffectType.POISON);
        }
    }
}
