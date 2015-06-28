using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class Hammer : Weapon
    {
        public Hammer(string name, Attack attack)
            : base(name, attack)
        {
            name = string.Format(name, "Hammer");
            this.icon = GameManager.Instance.Icons["hammer"];
        }
    }
}
