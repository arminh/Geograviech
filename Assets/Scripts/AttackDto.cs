using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class AttackDto
    {

        private FightCharacter attackedChar;
        private Effect currentEffect;
        private Effect inflictEfect;
        private int inflictedDamage;

        public FightCharacter getAttackedChar()
        {
            return attackedChar;
        }

        public void setAttackedChar(FightCharacter attackedChar)
        {
            this.attackedChar = attackedChar;
        }

        public Effect getCurrentEffect()
        {
            return currentEffect;
        }

        public void setCurrentEffect(Effect currentEffect)
        {
            this.currentEffect = currentEffect;
        }

        public Effect getInflictEffect()
        {
            return inflictEfect;
        }

        public void setInflictEffect(Effect inflictEfect)
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
