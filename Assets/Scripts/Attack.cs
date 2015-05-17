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

        public Attack(string name,ElementType type, int damage,Effect effect )
        {
            this.name = name;
            this.type = type;
            this.damage = damage;
            this.effect = effect;
        }

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
