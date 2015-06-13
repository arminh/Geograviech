using UnityEngine;
using System;
using System.Collections;

namespace Assets.Scripts
{
    public class StunEffect : Effect
    {
        public StunEffect(int inflictChance)
            : base(inflictChance, 10, EffectType.STUN)
        {

        }

        public override IEnumerator execute(FightCharacter character)
        {
            if (!tryCure(character))
            {
                increaseCureChance(15);
            }

            yield break;
        }

        protected override void playAnimation(FightCharacter character)
        {
            throw new System.NotImplementedException();
        }

    }
}
