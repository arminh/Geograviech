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
    protected Effect effect;


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

    public abstract void applyEffect();

    public abstract void cureEffect(Consumable item);
}
