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
        private int inflictChance;
        private int cureChance;

        Random rand = new Random();

        public Effect.EffectType inflict(Character character)
        {
            EffectType effect = character.getEffect();

            if (effect != EffectType.NONE)
            {
                return effect;
            }
            else 
            {
                int num = rand.Next(1, 100);

                if (num <= inflictChance)
                {
                    character.applyEffect(effect);
                    return this.type;
                }
                else
                {
                    return EffectType.NONE;
                }
            }
        }

        public void execute(Character character)
        {
            switch (type)
            {
                case Effect.EffectType.FIRE:

                    break;
                case Effect.EffectType.FREEZE:
  
                    break;
                case Effect.EffectType.POISON:

                    break;
                case Effect.EffectType.SLEEP:
     
                    break;
                case Effect.EffectType.STUN:
  
                    break;
            }
        }

        public enum EffectType { FIRE, POISON, SLEEP, STUN, FREEZE, NONE };
    }
}
