using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class SleepEffect : Effect
    {
        public SleepEffect(int inflictChance)
            : base(inflictChance, EffectType.SLEEP)
        {

        }

        public override void execute(FightCharacter character)
        {

        }
    }
}
