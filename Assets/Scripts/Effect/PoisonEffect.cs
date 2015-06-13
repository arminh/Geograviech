using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class PoisonEffect : Effect
    {
        public PoisonEffect(int inflictChance)
            : base(inflictChance, EffectType.POISON)
        {

        }

        public override IEnumerable execute(FightCharacter character)
        {
            yield break;
        }
    }
}
