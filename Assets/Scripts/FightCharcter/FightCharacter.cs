using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public abstract class FightCharacter
    {
        protected int health;
        protected int maxHealth;
        protected int speed;
        protected int strength;

        protected List<Attack> attacks;
        protected Effect currentEffect;

        GameObject sprite = null;
        Sprite icon = null;
       
        protected bool dead;

        private bool isEnemy = false;
        protected LifeBar lifeBar;

        protected string name;

        public FightCharacter(int maxHealth, int speed, int strength, string name, List<Attack> attacks, GameObject sprite, Sprite icon)
        {
            this.maxHealth = maxHealth;
            this.health = maxHealth;
            this.speed = speed;
            this.strength = strength;
            this.name = name;
            this.attacks = attacks;

            this.sprite = sprite;
            this.icon = icon;
        }
       

        protected int applyDamage(int damage)
        {
            //Reduce damage by Strength maybe?
            //damage -= strength

            health -= damage;
            lifeBar.Health = health;

            if(health <= 0) {
                die();
            }

            sprite.GetComponentInChildren<AnimationStatus>().PlaySpecialDamageEffect(Effect.EffectType.NONE);
            
            return damage;

        }

        protected void die()
        {
            dead = true;
            currentEffect = null;
            AnimationStatus animStatus = sprite.GetComponentInChildren<AnimationStatus>();
            animStatus.ResetSpecialDamageEffect();
            animStatus.Die();
        }

        public bool heal(int amount)
        {
            if (health < maxHealth)
            {
                health = Mathf.Min(health + amount, maxHealth);
                lifeBar.Health = health;
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
            applyDamage(attack.Damage);

            attack.Effect.inflict(this);
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
            get { return sprite; }
            set 
            { 
                sprite = value;
                lifeBar = sprite.GetComponentInChildren<LifeBar>();
                lifeBar.MaxHealth = maxHealth;
            }
        }

        public Sprite Icon
        {
            get { return icon; }
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
