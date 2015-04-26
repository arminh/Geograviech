using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class NewBehaviourScript : MonoBehaviour
    {

        private Character attackedChar;
        private Effect.EffectType currentEffect;
        private Effect.EffectType inflictEfect;
        private int inflictedDamage;

        private Character getAttackedChar() {
            return attackedChar;
        }

        private void setAttackedChar(Character attackedChar)
        {
            this.attackedChar = attackedChar;
        }

        private Effect.EffectType getCurrentEffect()
        {
            return currentEffect;
        }

        private void setCurrentEffect(Effect.EffectType currentEffect)
        {
            this.currentEffect = currentEffect;
        }

        private Effect.EffectType getInflictEffect()
        {
            return inflictEfect;
        }

        private void setInflictEffect(Effect.EffectType inflictEfect)
        {
            this.inflictEfect = inflictEfect;
        }

        private int getInflictedDamage()
        {
            return inflictedDamage;
        }

        private void setInflictedDamage(int inflictedDamage)
        {
            this.inflictedDamage = inflictedDamage;
        }
    }
}
