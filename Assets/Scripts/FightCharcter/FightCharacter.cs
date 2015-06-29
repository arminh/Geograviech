using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Assets.Scripts.Utils;
using Assets.Scripts.Effects;

namespace Assets.Scripts.FightCharacters
{
    public abstract class FightCharacter
    {
        protected int health;
        protected int maxHealth;
        protected int speed;
        protected int strength;

        protected List<Attack> attacks;
        protected Effect currentEffect;

        GameObject prefab = null;
        Sprite icon = null;
       
        protected bool dead;

        private bool isEnemy = false;
        protected LifeBar lifeBar;

        protected string name;

        protected string prefabId;
        protected string iconId;

        public FightCharacter(int maxHealth, int speed, int strength, string name, List<Attack> attacks, string prefabId, string iconId)
        {
            this.maxHealth = maxHealth;
            this.health = maxHealth;
            this.speed = speed;
            this.strength = strength;
            this.name = name;
            this.attacks = attacks;

            this.prefabId = prefabId;
            this.iconId = iconId;
            this.prefab = GameManager.Instance.Prefabs[prefabId];
            this.icon = GameManager.Instance.Icons[iconId];
        }
       

        protected void applyDamage(int damage)
        {
			if(damage < 0)
			{
				damage = 0;
			}

            prefab.GetComponentInChildren<AnimationStatus>().PlayNormalDamageEffect();

            Log.Instance.Info(name + " suffers " + damage + " damage.");

            health -= damage;
            lifeBar.Health = health;

            if(health <= 0) {
                die();
            }

        }

        protected void die()
        {
            dead = true;
            currentEffect = null;
            AnimationStatus animStatus = prefab.GetComponentInChildren<AnimationStatus>();
            animStatus.ResetSpecialDamageEffect();
            animStatus.Die();

            Log.Instance.Info(Name + " died heroically in battle and will ascend to Viechhalla.");
        }

        public bool heal(int amount)
        {
            if (health < maxHealth)
            {
                int healthBefore = health;
                health = Mathf.Min(health + amount, maxHealth);

                lifeBar.Health = health;

                int healAmount = health - healthBefore;
                Log.Instance.Info(name + " was healed for " + healAmount + " damage.");

                return true;
            }
            return false;
        }

        public bool revive(int healAmount)
        {
            if (dead)
            {
                dead = false;
                health = healAmount;
                lifeBar.Health = health;
                Log.Instance.Info(name + " was denied a eternal life of hapiness in Viechhalla and is now alive again.");
                //TODO notify AnimationHandler
                return true;
            }
            return false;
        }

        public bool cureEffect(Effect.EffectType effectToCure)
        {
            if (effectToCure.Equals(currentEffect.Type))
            {
                currentEffect.cure(this);
                return true;
            }
            return false;
        }

        public void getAttacked(Attack attack, int opponentStrength)
        {
            applyDamage(attack.Damage + opponentStrength - Strength);

            if (!dead)
            {
                if (attack.Effect != null)
                {
                    attack.Effect.inflict(this);
                }
                   
            }
        }

        public int Health
        {
            get { return health; }
            set { health = value;
            lifeBar.Health = health;
            }
        }

        public int MaxHealth
        {
            get { return maxHealth; }
        }

        public int Speed
        {
            get { return speed; }
        }

        public int Strength
        {
            get { return strength; }
        }

        public string Name
        {
            get { return name; }
        }
        
        public List<Attack> Attacks
        {
            get { return attacks; }
        }

        public Effect CurrentEffect
        {
            get { return currentEffect; }
            set { currentEffect = value; }
        }

        public GameObject Sprite
        {
            get { return prefab; }
            set 
            {
                prefab = value;
                lifeBar = prefab.GetComponentInChildren<LifeBar>();
                lifeBar.MaxHealth = maxHealth; 
                lifeBar.Health = health;
            }
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

        public bool IsEnemy
        {
            get { return isEnemy; }
            set { isEnemy = value; }
        }

        public bool isDead()
        {
            return dead;
        }
    }
}
