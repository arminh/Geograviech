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

        protected Weapon(string name, Attack attack) : base(name)
        {
            this.attack = attack;
        }

        public Weapon(string name, Attack attack, String icon)
            : base(name)
        {
            this.attack = attack;
            this.icon = GameManager.Instance.Icons[icon];
        }

        public Attack Attack
        {
            get { return attack;  }
        }
    }
}
