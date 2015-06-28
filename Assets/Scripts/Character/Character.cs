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

        protected string prefabId;
        protected string iconId;

        protected GameObject prefab;
        protected Sprite icon;

        public Character(int maxHealth, int currentHealth, int speed, int strength, string name, int level, int xp, List<Attack> attacks, string prefabId, string iconId)
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
            this.prefabId = prefabId;
            this.iconId = iconId;
			if(prefabId != null)
            	this.prefab = GameManager.Instance.Prefabs[prefabId];
			if(iconId != null)
				this.icon = GameManager.Instance.Icons[iconId];
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
            get { return prefab; }
        }

        public Sprite Icon
        {
            get { return icon; }
        }

        public string PrefabId
        {
            get { return prefabId; }
        }

        public string IconId
        {
            get { return iconId; }
        }
    }
}