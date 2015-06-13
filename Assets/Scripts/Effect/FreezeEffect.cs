using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class FreezeEffect : Effect
    {
        public FreezeEffect(int inflictChance)
            : base(inflictChance, 10, EffectType.FREEZE)
        {

        }

        public override IEnumerable execute(FightCharacter character)
        {
            int numAttacks = character.Attacks.Count;

            int activeAttack = rand.Next(0, numAttacks-1);

            for (int i = 0; i < numAttacks; i++)
            {
                if (i != activeAttack)
                {
                    character.Attacks[i].Active = false;
                }
            }

            yield break;
        }


        public override void cure(FightCharacter character)
        {
            base.cure(character);

            foreach (Attack attack in character.Attacks)
            {
                attack.Active = true;
            }
        }
    }
}
