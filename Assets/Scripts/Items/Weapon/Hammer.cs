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
            : base(string.Format(name, "Hammer"), attack, "hammer")
        {
        }
    }
}
