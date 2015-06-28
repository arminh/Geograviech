using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class Sword : Weapon
    {
        public Sword(string name, Attack attack)
            : base(name, attack)
        {
            name = string.Format(name, "Sword");
            this.icon = GameManager.Instance.Icons["normalAttack"];
        }
    }
}
