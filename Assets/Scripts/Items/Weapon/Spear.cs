using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class Spear : Weapon
    {
        public Spear(string name, Attack attack)
            : base(name, attack)
        {
            name = string.Format(name, "Spear");
           this.icon = GameManager.Instance.Icons["spear"];
        }
    }
}
