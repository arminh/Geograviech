using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Assets.Scripts.Effects;
using Assets.Scripts.FightCharacters;

namespace Assets.Scripts.Items.Consumables
{
    class BurnHealer : Item, IConsumable
    {
        public BurnHealer()
            : base("Bepanthen Wund und Heilsalbe")
        {
            this.icon = GameManager.Instance.Icons["heilsalbe"];
            this.description = "Helps against burnings.";
        }
        public bool use(FightCharacter character)
        {
            return character.cureEffect(Effect.EffectType.BURN);
        }
    }
}
