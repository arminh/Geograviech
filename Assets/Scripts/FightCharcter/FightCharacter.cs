﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public abstract class FightCharacter
    {

        public string identifier;

        protected int health;
        protected int maxHealth;
        protected int speed;
        protected int strength;

        protected List<Attack> attacks;
        protected Effect currentEffect;

        GameObject sprite = null;
       
        protected bool dead;

        private bool isEnemy = false;

        protected AttackDto attackResult;

        protected string name;

        public FightCharacter(string identifier, int maxHealth, int speed, int strength, string name, List<Attack> attacks)
        {
            this.identifier = identifier;
            this.maxHealth = maxHealth;
            this.health = maxHealth;
            this.speed = speed;
            this.strength = strength;
            this.name = name;
            this.attacks = attacks;
        }
       

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


        public bool cureEffect(Effect.EffectType effectToCure)
        {
            if (effectToCure.Equals(currentEffect.Type))
            {
                currentEffect = null;
                //TODO notify AnimationHandler
                return true;
            }
            return false;
        }

        public AttackDto getAttacked(Attack attack)
        {
            AttackDto attackResult = new AttackDto();
            attackResult.setAttackedChar(this);
            attackResult.setInflictedDamage(applyDamage(attack.Damage));

            attack.Effect.inflict(this);
            attackResult.setCurrentEffect(currentEffect);
            attackResult.setInflictEffect(attack.Effect);

            return attackResult;
        }


        public string Identifier
        {
            get { return identifier; }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
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
            set { sprite = value; }
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
