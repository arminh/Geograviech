using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Character
{
    public abstract class Character
    {
        protected string name;

        protected int maxHealth;
        protected int currentHealth;
        protected int speed;
        protected int strength;

        protected int level;
        protected int xp;
        protected int levelUpXp;

        protected List<Attack> attacks;

        protected GameObject sprite;
        protected Sprite icon;

        public Character(int maxHealth, int currentHealth, int speed, int strength, string name, int level, int xp, List<Attack> attacks, GameObject sprite, Sprite icon)
        {
            this.maxHealth = maxHealth;
            this.currentHealth = currentHealth;
            this.speed = speed;
            this.strength = strength;
            this.name = name;
            this.level = level;
            this.xp = xp;
            this.levelUpXp = 1000 * level;
            this.attacks = attacks;
            this.sprite = sprite;
            this.icon = icon;
        }


        public void gainXp(int amount)
        {
            xp += amount;

            if (xp > levelUpXp)
            {
                levelUp();
            }
        }

        protected abstract void levelUp();

        protected void updateStats()
        {

        }

        public string Name
        {
            get { return name; }
        }

        public int MaxHealth
        {
            get { return maxHealth; }
        }

        public int CurrentHealth
        {
            get { return currentHealth; }
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

        public List<Attack> Attacks
        {
            get { return attacks; }
        }

        public GameObject Sprite
        {
            get { return sprite; }
        }

        public Sprite Icon
        {
            get { return icon; }
        }
    }
}