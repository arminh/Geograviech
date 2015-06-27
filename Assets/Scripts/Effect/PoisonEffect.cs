using System;
using System.Collections;

using Assets.Scripts.Utils;
using Assets.Scripts.FightCharacters;

namespace Assets.Scripts.Effects
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
                Log.Instance.Info(character.Name + " suffered " + damage + " from poison effect.");

            }

            yield break;
        }
    }
}
