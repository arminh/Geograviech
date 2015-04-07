using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class Effect
    {
        private string name;
        private EffectType type;
        private float inflictChance;

        public void inflict()
        {

        }

        public void execute(Character character)
        {

        }

        public enum EffectType { FIRE, POISON, SLEEP, STUN, FREEZE };
    }
}
