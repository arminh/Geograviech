﻿using UnityEngine;
using System;
using System.Collections;

namespace Assets.Scripts
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
