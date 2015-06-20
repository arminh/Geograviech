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

        public override void cure(FightCharacter character)
        {
            base.cure(character);
            character.Sprite.GetComponent<AnimationStatus>().WakeUp();
        }

        public override void inflict(FightCharacter character)
        {
            base.inflict(character);
            character.Sprite.GetComponent<AnimationStatus>().FallAsleep();
        }
    }
}
