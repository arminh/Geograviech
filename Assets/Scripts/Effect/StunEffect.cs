using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class StunEffect : Effect
    {
        public StunEffect(int inflictChance)
            : base(inflictChance, EffectType.STUN)
        {

        }

        public override void execute(FightCharacter character)
        {

        }
    }
}
