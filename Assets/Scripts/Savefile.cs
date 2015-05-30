using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class Savefile {

        public string name;
        public string identifier;

        public int maxHealth;
        public int speed;
        public int strength;

        public int level;
        public int xp;

        public List<Attack> attacks;

        public List<Viech> viecher;
        public List<Viech> activeViecher;

        public List<Weapon> weapons;
        public Weapon activeWeapon;

        public List<IItem> items;

    }
}