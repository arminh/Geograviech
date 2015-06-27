using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Assets.Scripts.Utils;
using Assets.Scripts.Effects;

namespace Assets.Scripts
{

    public class Attack
    {
        private string name;
        private Enums.ElementType type;
        private int minDamage;
        private int maxDamage;
        int cooldownRounds;
        private Effect effect;
        private bool active = true;

        private int level;
        private Sprite icon;

        protected System.Random rand;

		public Attack(Attack copy)
		{
			this.name = copy.Name;
			this.type = copy.Type;
            this.minDamage = copy.Damage;
			this.cooldownRounds = copy.cooldownRounds;
			this.effect = copy.Effect;
			this.icon = copy.Icon;
			this.level = copy.Level;
		}

        public Attack(string name, Enums.ElementType type, int minDamage, int maxDamage, int cooldownRounds, Effect effect, Sprite icon, int level = 1)
        {
            this.name = name;
            this.type = type;
            this.minDamage = minDamage;
            this.maxDamage = maxDamage;
            this.cooldownRounds = cooldownRounds;
            this.effect = effect;
            this.icon = icon;
            this.level = level;
            this.rand = new System.Random();
        }

        public string Name
        {
            get { return name;  }
        }

        public Enums.ElementType Type
        {
            get { return type; }
        }

        public int Damage
        {
            get { return rand.Next(minDamage, maxDamage) * level; }
        }

        public int MinDamage
        {
            get { return minDamage; }
        }
        
        public int MaxDamage
        {
            get { return maxDamage; }
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
