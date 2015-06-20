using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{

    public class Attack
    {
        private string name;
        private ElementType type;
        private int damage;
        private Effect effect;
        private bool active = true;

        private int level;
        private Sprite icon;

        public Attack(string name,ElementType type, int damage, Effect effect, Sprite icon, int level = 0)
        {
            this.name = name;
            this.type = type;
            this.damage = damage;
            this.effect = effect;
            this.icon = icon;
            this.level = level;
        }

        public string Name
        {
            get { return name;  }
        }

        public ElementType Type
        {
            get { return type; }
        }

        public int Damage
        {
            get { return damage * level; }
        }

        public Effect Effect
        {
            get { return effect; }
        }

        public Sprite Icon
        {
            get { return icon; }
        }

        public int Level 
        {
            get { return level; }
            set { level = value; }
        }

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

    }
}
