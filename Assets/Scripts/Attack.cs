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
        int cooldownRounds;
        private Effect effect;
        private bool active = true;

        private int level;
        private Sprite icon;

		public Attack(Attack copy)
		{
			this.name = copy.Name;
			this.type = copy.Type;
			this.damage = copy.Damage;
			this.cooldownRounds = copy.cooldownRounds;
			this.effect = copy.Effect;
			this.icon = copy.Icon;
			this.level = copy.Level;
		}

        public Attack(string name,ElementType type, int damage, int cooldownRounds, Effect effect, Sprite icon, int level = 1)
        {
            this.name = name;
            this.type = type;
            this.damage = damage;
            this.cooldownRounds = cooldownRounds;
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
