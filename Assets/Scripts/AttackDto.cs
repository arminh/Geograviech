using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class AttackDto : MonoBehaviour
    {

        private Character attackedChar;
        private Effect.EffectType currentEffect;
        private Effect.EffectType inflictEfect;
        private int inflictedDamage;

        public Character getAttackedChar()
        {
            return attackedChar;
        }

        public void setAttackedChar(Character attackedChar)
        {
            this.attackedChar = attackedChar;
        }

        public Effect.EffectType getCurrentEffect()
        {
            return currentEffect;
        }

        public void setCurrentEffect(Effect.EffectType currentEffect)
        {
            this.currentEffect = currentEffect;
        }

        public Effect.EffectType getInflictEffect()
        {
            return inflictEfect;
        }

        public void setInflictEffect(Effect.EffectType inflictEfect)
        {
            this.inflictEfect = inflictEfect;
        }

        public int getInflictedDamage()
        {
            return inflictedDamage;
        }

        public void setInflictedDamage(int inflictedDamage)
        {
            this.inflictedDamage = inflictedDamage;
        }
    }
}
