using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{

    public class Attack
    {
        private string name;
        private ElementType type;
        private int damage;
        private Effect effect;

        public void execute(FightCharacter character)
        {

        }

        public int getDamage()
        {
            return damage;
        }

        public void setDamage(int value)
        {
            damage = value;
        }

        public ElementType getType()
        {
            return type;
        }

        public void setType(ElementType value)
        {
            type = value;
        }

        public Effect getEffect()
        {
            return effect;
        }

        public void setEffect(Effect value)
        {
            effect = value;
        }

    }
}
