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
            : base(string.Format(name, "Spear"), attack, "spear")
        {
        }
    }
}
