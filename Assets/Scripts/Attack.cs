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

        public string Name
        {
            get { return name;  }
        }

        public ElementType Type
        {
            get { return type; }
        }

        public int Damage
        {
            get { return damage; }
        }

        public Effect Effect
        {
            get { return effect; }
        }

    }
}
