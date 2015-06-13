using UnityEngine;
using System;
using System.Collections;

namespace Assets.Scripts
{
    public class SleepEffect : Effect
    {
        public SleepEffect(int inflictChance)
            : base(inflictChance, 10, EffectType.SLEEP)
        {

        }

        public override IEnumerator execute(FightCharacter character)
        {
            if (!tryCure(character))
            {
                increaseCureChance(15);
                yield return new WaitForSeconds(3);
            }

            yield break;
        }

        protected override void playAnimation(FightCharacter character)
        {
            throw new System.NotImplementedException();
        }
    }
}
