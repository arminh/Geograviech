using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Utils;

namespace Assets.Scripts
{
    public class FreezeEffect : Effect
    {
        public FreezeEffect(int inflictChance)
            : base(inflictChance, 10, EffectType.FREEZE)
        {
            inflictMsg = "frozen";
        }

        public override IEnumerator execute(FightCharacter character)
        {
            int numAttacks = character.Attacks.Count;

            int activeAttack = rand.Next(0, numAttacks-1);

            string log = character.Name + "'s attacks ";

            for (int i = 0; i < numAttacks; i++)
            {
                if (i != activeAttack)
                {
                    character.Attacks[i].Active = false;

                    log += character.Attacks[i].Name;

                    if (i == numAttacks - 2)
                    {
                        log += " and ";
                    }
                    else if (i == numAttacks -1)
                    {
                        log += " ";
                    }
                    else
                    {
                        log += ", ";
                    }
                }
            }

            log += "are disabled.";
            Log.Instance.Info(log);

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
