using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;

public abstract class Character : MonoBehaviour {

    protected string name;
    protected int level;
    protected int health;
    protected int maxHealth;
    protected int speed;
    protected int strength;
    protected List<Attack> attacks;
    protected Effect.EffectType effect;


	// Use this for initialization
	void Start () {
	
	}

    public void addXP(int amount)
    {

    }

    public abstract void executeTurn();

    protected abstract void die();

    public abstract void heal(int amount);

    public abstract void levelUp();

    public abstract void revive(int healAmount);

    public abstract void applyEffect();

    public abstract void cureEffect(Effect.EffectType effect);


}
