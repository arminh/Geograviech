using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class Ax : Weapon
    {
        public Ax(string name, Attack attack)
            : base(name, attack)
        {
            name = string.Format(name, "Ax");
            this.icon = GameManager.Instance.Icons["ax"];
        }
    }
}
