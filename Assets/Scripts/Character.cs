using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;
using Assets.Scripts.Utils;

namespace Assets.Scripts
{
    public abstract class Character : MonoBehaviour
    {

        public Enums.MonsterStatus Status;

        protected new string name;
        protected int level;
        protected int health;
        protected int maxHealth;
        protected int speed;
        protected int strength;
        protected List<Attack> attacks;
        protected Effect.EffectType effect;
        protected bool dead;
        protected int xp;
        protected int levelUpXp;

        private bool isEnemy = false;

        GameObject sprite = null;

        protected AttackDto attackResult;

        // Use this for initialization
        void Start()
        {
            effect = Effect.EffectType.NONE;

        }


        public void addXP(int amount)
        {
            xp = xp + amount;

            if (xp >= levelUpXp)
            {
                this.levelUp();
            }

        }

        public abstract void executeTurn();

        protected int applyDamage(int damage)
        {
            //Reduce damage by Strength maybe?
            //damage -= strength;

            health -= damage;

            return damage;

        }

        protected abstract void die();

        public bool heal(int amount)
        {
            if (health < maxHealth)
            {
                health = Mathf.Min(health + amount, maxHealth);
                return true;
            }
            return false;
        }

        public abstract void levelUp();

        public bool revive(int healAmount)
        {
            if (dead)
            {
                dead = false;
                health = healAmount;
                //TODO notify AnimationHandler
                return true;
            }
            return false;
        }

        public Effect.EffectType applyEffect(Effect.EffectType effectToApply)
        {

            if (effect != Effect.EffectType.NONE)
            {
                return effect;
            }

            switch (effectToApply)
            {
                case Effect.EffectType.FIRE:
                    sprite.GetComponent<AnimationStatus>().SetOnFire = true;
                    break;
                case Effect.EffectType.FREEZE:
                    sprite.GetComponent<AnimationStatus>().SetFrozen = true;
                    break;
                case Effect.EffectType.POISON:
                    sprite.GetComponent<AnimationStatus>().SetPoisoned = true;
                    break;
                case Effect.EffectType.SLEEP:
                    sprite.GetComponent<AnimationStatus>().SetSleeping = true;
                    break;
                case Effect.EffectType.STUN:
                    sprite.GetComponent<AnimationStatus>().SetStunned = true;
                    break;
            }

            return effectToApply;
        }

        public bool cureEffect(Effect.EffectType effectToCure)
        {
            if (effectToCure.Equals(effect))
            {
                effect = Effect.EffectType.NONE;
                //TODO notify AnimationHandler
                return true;
            }
            return false;
        }

        public abstract AttackDto getAttacked(Attack attack); // returns the inflickted damage

        public abstract bool isElite();

        public int getSpeed()
        {
            return speed;
        }

        public string getName()
        {
            return name;
        }

        public void setIsEnemy(bool isEnemy)
        {
            this.isEnemy = isEnemy;
        }

        public bool getIsEnemy()
        {
            return isEnemy;
        }

        public void setSprite(GameObject sprite)
        {
            this.sprite = sprite;
        }

        public GameObject getSprite()
        {
            return sprite;
        }

        public Effect.EffectType getEffect()
        {
            return effect;
        }

        public bool isDead()
        {
            return dead;
        }
    }
}


