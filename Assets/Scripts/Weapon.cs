using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Weapon: Item
    {
        private Attack attack;

        public Weapon(Attack attack)
        {
            this.attack = attack;
        }

        protected void use()
        {
            throw new NotImplementedException();
        }

        public Attack Attack
        {
            get { return attack;  }
        }
    }
}
