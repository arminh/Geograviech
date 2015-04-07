using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Consumables
{
    class BurnHealer : Item, IConsumable
    {
        public BurnHealer()
        {
            this.name = "Bepanthen Wund und Heilsalbe";
        }
        public void use(Character character)
        {
            character.cureEffect(Effect.EffectType.FIRE);
        }
    }
}
