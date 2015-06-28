using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class Weapon: Item
    {
        private Attack attack;

        public Weapon(string name, Attack attack) : base(name)
        {
            this.attack = attack;
        }

        public Weapon(string name, Attack attack, Sprite icon)
            : base(name)
        {
            this.attack = attack;
            this.icon = icon;
        }

        public Attack Attack
        {
            get { return attack;  }
        }
    }
}
