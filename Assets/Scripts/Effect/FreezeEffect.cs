using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class FreezeEffect : Effect
    {
        public FreezeEffect(int inflictChance)
            : base(inflictChance, EffectType.FREEZE)
        {

        }

        public override void execute(FightCharacter character)
        {

        }
    }
}
