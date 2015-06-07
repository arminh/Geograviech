using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public abstract class Effect
    {
        private EffectType type;
        private int inflictChance;

        public Effect(int inflictChance, EffectType type)
        {
            this.inflictChance = inflictChance;
            this.type = type;
        }

        Random rand = new Random();

        public void inflict(FightCharacter character)
        {
            if (character.CurrentEffect == null)
            {
                int num = rand.Next(1, 100);

                if (num <= inflictChance)
                {
                    character.CurrentEffect = this;
                }
            }
        }

        public abstract void execute(FightCharacter character);

        public enum EffectType { BURN, POISON, SLEEP, STUN, FREEZE, NONE };

        public EffectType Type
        {
            get { return type;  }
        }

        public int InflictChance
        {
            get { return inflictChance; }
        }
    }
}
