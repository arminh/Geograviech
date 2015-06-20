using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Weapon: Item
    {
        private Attack attack;

        public Weapon(string name, Attack attack, Sprite icon) : base(name, 1, 1, icon)
        {
            this.attack = attack;
        }

        public Attack Attack
        {
            get { return attack;  }
        }
    }
}
