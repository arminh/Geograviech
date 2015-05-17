using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class Character
    {
        protected string name;
        protected string identifier;

        protected int maxHealth;
        protected int speed;
        protected int strength;

        protected int level;
        protected int xp;
        protected int levelUpXp;

        protected List<Attack> attacks;

        public Character(int maxHealth, int speed, int strength, string name, string identifier, int level, int xp, List<Attack> attacks)
        {
            this.maxHealth = maxHealth;
            this.speed = speed;
            this.strength = strength;
            this.name = name;
            this.identifier = identifier;
            this.level = level;
            this.xp = xp;
            this.levelUpXp = 1000 * level;
            this.attacks = attacks;
        }


        public void gainXp(int amount)
        {

        }

        public string Name
        {
            get { return name; }
        }

        public string Identifier
        {
            get { return identifier; }
        }

        public int MaxHealth
        {
            get { return MaxHealth; }
        }

        public int Speed
        {
            get { return speed; }
        }

        public int Strength
        {
            get { return strength; }
        }

        public int Level
        {
            get { return level; }
        }

        public int Xp
        {
            get { return xp; }
        }
    }
}