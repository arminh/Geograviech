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

        public override void execute(FightCharacter character)
        {

        }
    }
}
