using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class BurnEffect: Effect
    {
        public BurnEffect(int inflictChance)
            : base(inflictChance, EffectType.BURN)
        {

        }

        public override IEnumerable execute(FightCharacter character)
        {
            yield break;
        }
    }
}
