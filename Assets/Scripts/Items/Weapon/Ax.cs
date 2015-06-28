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
            : base(string.Format(name, "Ax"), attack)
        {
            this.icon = GameManager.Instance.Icons["ax"];
        }
    }
}
