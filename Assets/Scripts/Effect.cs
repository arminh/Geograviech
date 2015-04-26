using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Effect
    {
        private string name;
        private EffectType type;
        private float inflictChance;

        public Effect.EffectType inflict(Character character)
        {
            return character.applyEffect(type);
        }

        public void execute(Character character)
        {

        }

        public enum EffectType { FIRE, POISON, SLEEP, STUN, FREEZE, NONE };
    }
}
