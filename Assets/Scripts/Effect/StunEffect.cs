using UnityEngine;
using System;
using System.Collections;

using Assets.Scripts.FightCharacters;

namespace Assets.Scripts.Effects
{
    public class StunEffect : Effect
    {
        public StunEffect(int inflictChance)
            : base(inflictChance, 10, EffectType.STUN)
        {
            inflictMsg = "confused";
        }

        public override IEnumerator execute(FightCharacter character)
        {
            if (!tryCure(character))
            {
                increaseCureChance(15);
            }

            yield break;
        }
    }
}
