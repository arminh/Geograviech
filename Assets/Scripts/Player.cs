using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        private int health;
        private int speed;
        private int strength;

        private int xp;
        private int level;

        private List<Viech> viecher;
        private List<Viech> activeViecher;
        
        private List<Weapon> weapons;
        private Weapon activeWeapon;

        private List<IConsumable> items;

        public Player(int health, int speed, int strength, int xp, int level, List<Viech> viecher, List<Viech> activeViecher, List<Weapon> weapons, Weapon activeWeapon)
        {
            this.health = health;
            this.speed = speed;
            this.strength = strength;
            this.xp = xp;
            this.level = level;
            this.viecher = viecher;
            this.activeViecher = activeViecher;
            this.weapons = weapons;
            this.activeWeapon = activeWeapon;
        }

        public Hero createHero()
        {
            return new Hero(activeViecher, activeWeapon, items, health, speed, strength);
        }

        public void gainXp(int amount)
        {

        }

        public List<Viech> getViecher() {
            return viecher;
        }
        public void setViecher(List<Viech> value) {
            viecher = value;
        }

        public List<Viech> getActiveViecher() {
            return activeViecher;
        }
        public void setActiveViecher(List<Viech> value) {
            activeViecher = value;
        }

        public List<Weapon> getWeapons() {
            return weapons;
        }
        public void setWeapons(List<Weapon> value) {
            weapons = value;
        }

        public Weapon getActiveWeapon() {
            return activeWeapon;
        }
        public void setActiveWeapon(Weapon value) {
             activeWeapon = value;
        }
    }
}
