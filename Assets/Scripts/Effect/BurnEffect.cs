using UnityEngine;
using System;
using System.Collections;

using Assets.Scripts.Utils;
using Assets.Scripts.FightCharacters;

namespace Assets.Scripts.Effects
{
    public class BurnEffect: Effect
    {
        

        public BurnEffect(int inflictChance)
            : base(inflictChance, 10, EffectType.BURN)
        {
            inflictMsg = "burning";
        }

        public override IEnumerator execute(FightCharacter character)
        {
            if (!tryCure(character))
            {
                increaseCureChance(15);

                int health = character.Health;
                double dmgTemp = (double)health / 100 * 5;
                int damage = Convert.ToInt32(dmgTemp);

                character.Health -= damage;
                Log.Instance.Info(character.Name + " suffered " + damage + " from burn effect.");
            }

            yield break;
        }


        protected override void applyEffect(FightCharacter character)
        {
            character.CurrentEffect = new BurnEffect(this.inflictChance);
        }
    }
}
