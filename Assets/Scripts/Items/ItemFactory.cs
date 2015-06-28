using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

using Assets.Scripts.Utils;
using Assets.Scripts.Effects;
using Assets.Scripts.Items.Consumables;

namespace Assets.Scripts.Items
{
    class ItemFactory
    {
        const int numConsumables = 7;

        System.Random rand = new System.Random();

        public List<Item> createRandomDrops(int level, int amount)
        {
            List<Item> drops = new List<Item>();
            
            int roll;

            for(int i = 0; i < amount-1; i++)
            {
                roll = rand.Next(1, 100);

                if (roll <= 70)
                {
                    IConsumable item = createRandomConsumable(level);
                    if (item != null)
                    {
                        drops.Add((Item)item);
                    }
                } 
                else
                {
                    Weapon item = createRandomWeapon(level);
                    if (item != null)
                    {
                        drops.Add((Item)item);
                    }
                }
            }

            return drops;
        } 

        private IConsumable createRandomConsumable(int level) 
        {
            int dropRoll = rand.Next(1, 100);
            IConsumable drop = null;

            switch(rand.Next(1, numConsumables)) 
            {
                case 1:
                    drop = new Antidote();  
                    break;
                case 2:
                    drop = new Awakener();
                    break;
                case 3:
                    drop = new BurnHealer();
                    break;
                case 4:
                    drop = new FreezeHealer();
                    break;
                case 5:
                    drop = new HealPotion(level * 10);
                    break;
                case 6:
                    drop = new Reviver(level * 3);
                    break;
                case 7:
                    drop = new StunHealer();
                    break;
            }
            
            if(dropRoll <= 50) 
            { 
                return drop;
            }  
            else
            {
                return null;
            }
        }

        private Weapon createRandomWeapon(int level)
        {
            int minDamage = 0;
            int maxDamage = 0;
            double dropChance = 1.0;
            int coolDownRounds = 0;
            Effect effect = null;
            Sprite icon = null;

            string name = "Sword";
            string namePrefix = "";
            string nameSuffix = "";


            int power = rand.Next(1, 5);

            switch(power) {
                case 1:
                    minDamage = 2;
                    maxDamage = 4;
                    break;

                case 2:
                    minDamage = 3;
                    maxDamage = 5;
                    nameSuffix = " of Boon";
                    dropChance *= 0.8;
                    break;

                case 3:
                    minDamage = 4;
                    maxDamage = 6;
                    nameSuffix = " of Averageness";
                    dropChance *= 0.6;
                    break;

                case 4:
                    minDamage = 6;
                    maxDamage = 10;
                    nameSuffix = " of the Gods";
                    dropChance *= 0.3;
                    break;

                case 5:
                    minDamage = 10;
                    maxDamage = 20;
                    nameSuffix = " of Eternal Devestation";
                    dropChance *= 0.05;
                    break;
            }

            int effectRoll = rand.Next(1, 10);
            int chance = rand.Next(1, 5);

            switch(effectRoll) 
            {
                case 1:
                    effect = new FreezeEffect(10 * chance);
                    name = "Ice-" + name;
                    break;
                case 2:
                    effect = new BurnEffect(10 * chance);
                    name = "Fire-" + name;
                    break;
                case 3:
                    effect = new PoisonEffect(10 * chance);
                    name = "Poison-" + name;
                    break;
                case 4:
                    effect = new SleepEffect(10 * chance);
                    name = "Bore-" + name;
                    break;
                case 5:
                    effect = new StunEffect(10 * chance);
                    name = "Confusion-" + name;
                    break;
            }
            
            if(effectRoll <= 5) {
                dropChance *= 0.7;
            }

            switch(chance) 
            {
                case 1:
                    namePrefix = "Unlucky ";
                    break;
                case 2:
                    namePrefix = "";
                    dropChance *= 0.9;
                    break;
                case 3:
                    namePrefix = "Average ";
                    dropChance *= 0.8;
                    break;
                case 4:
                    namePrefix = "Lucky ";
                    dropChance *= 0.7;
                    break;
                case 5:
                    namePrefix = "Fortuna's ";
                    dropChance *= 0.6;
                    break;
            }

            name = namePrefix + name + nameSuffix;

            int dropRoll = rand.Next(1, 100);

            if(dropRoll <= Math.Round(100*dropChance)) {
                return new Weapon(name, new Attack(name, Enums.ElementType.NORMAL, minDamage, maxDamage, coolDownRounds, effect, null, level), icon);
            }
            else
            {
                return null;
            }
            
        }
    }
}
