using System;
using System.Collections;
using Assets.Scripts.Utils;

namespace Assets.Scripts
{
    public class PoisonEffect : Effect
    {
        public PoisonEffect(int inflictChance)
            : base(inflictChance, 10, EffectType.POISON)
        {
            inflictMsg = "poisoned";
        }

        public override IEnumerator execute(FightCharacter character)
        {
            if (!tryCure(character))
            {
                increaseCureChance(15);

                int health = character.Health;
                int percentage = rand.Next(1, 10);
                double dmgTemp = health / 100 * percentage;
                int damage = Convert.ToInt32(dmgTemp);

                character.Health -= damage;
            }

            yield break;
        }
    }
}
