﻿using UnityEngine;
using System;
using System.Collections;

namespace Assets.Scripts
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
            }

            yield break;
        }
    }
}
