using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;

public abstract class Character : MonoBehaviour {

    protected new string name;
    protected int level;
    protected int health;
    protected int maxHealth;
    protected int speed;
    protected int strength;
    protected List<Attack> attacks;
    protected Effect.EffectType effect;
    protected bool dead;
    int xp;


	// Use this for initialization
	void Start () {
	
	}

    public void addXP(int amount)
    {

    }

    public abstract void executeTurn();

    protected virtual void die()
    {
        dead = true;
        health = 0;
        //TODO notify AnimationHandler 
    }

    public bool heal(int amount)
    {
        if(health < maxHealth)
        {
            health = Mathf.Min(health + amount, maxHealth);
            return true;
        }
        return false;
    }

    public abstract void levelUp();

    public bool revive(int healAmount)
    {
        if(dead)
        {
            dead = false;
            health = healAmount;
            //TODO notify AnimationHandler
            return true;
        }
        return false;
    }

    public bool applyEffect(Effect.EffectType effectToApply)
    {
        effect = effectToApply;
        return true;
    }

    public bool cureEffect(Effect.EffectType effectToCure)
    {
        if(effectToCure.Equals(effect))
        {
            effect = Effect.EffectType.NONE;
            //TODO notify AnimationHandler
            return true;
        }
        return false;
    }

    public abstract int getAttacked(Attack attack); // returns the inflickted damage

    public abstract bool isElite();

    public int getSpeed()
    {
        return speed;
    }
}
