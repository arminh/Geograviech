using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Assets.Scripts.Utils;
using Assets.Scripts.Effects;

namespace Assets.Scripts.FightCharacters
{
    class FightViechFactory
    {
        System.Random rand = new System.Random();

        public FightViechFactory()
        {
        }

        public FightViech createFightViech(Enums.ViechName viech)
        {
            FightViech monster = null;
            switch (viech)
            {
                case Enums.ViechName.Alraune:
                    monster = createAlraune (GameManager.Instance.getPlayer().Level);
                    break;
                case Enums.ViechName.Zerber:
                    monster = createZerberwelpe(GameManager.Instance.getPlayer().Level);
                    break;
                case Enums.ViechName.Sirene:
                    monster = createSirene(GameManager.Instance.getPlayer().Level);
                    break;
                case Enums.ViechName.Imp:
                    monster = createImp(GameManager.Instance.getPlayer().Level);
                    break;
                case Enums.ViechName.Gargoyle:
                    monster = createGargoyle(GameManager.Instance.getPlayer().Level);
                    break;
                case Enums.ViechName.Panther:
					monster = createPanther(GameManager.Instance.getPlayer().Level);
                    break;
            }

            return monster;
        }

        public FightViech createImp(int level)
        {
			/* INFO: Base stats:
             * maxHealth = level *10
             * speed = level;
             * strength = level;
             * 
             * Einer dieser stats wird mit 3 multipliziert, einer mit 2 und einer mit 1
             */
			
			
			//string identifier, int maxHealth, int speed, int strength, string name, List<Attack> attacks, ElementType type, int catchChance, List<Item> drops, int xpAmount
			Attack swipe = new Attack("Swipe", Enums.ElementType.NORMAL, 1, 3, 2, null, null, level);
			Attack claw = new Attack("Beware of my claws", Enums.ElementType.EARTH, 2, 4, 4, null, null, level);
			Attack leer = new Attack("Leer", Enums.ElementType.NORMAL, 1, 2, 5, new StunEffect(50), null, level);
			Attack explode = new Attack("BOOM", Enums.ElementType.EARTH, 5, 7, 10, null, null, level);
			List<Attack> attacks = new List<Attack> ();
			attacks.Add (swipe);
			attacks.Add (claw);
			attacks.Add (leer);
			attacks.Add (explode);
			
			String name = rand.Next(0, 5) == 0 ? "Tobby" : "Imp";
			
			return new FightViech(level * 10 , level * 3, level *2, name, attacks, Enums.ElementType.EARTH, 50, null, level * 50, "Imp", "ImpIcon");


        }

        public FightViech createGargoyle(int level)
        {
			/* INFO: Base stats:
             * maxHealth = level *10
             * speed = level;
             * strength = level;
             * 
             * Einer dieser stats wird mit 3 multipliziert, einer mit 2 und einer mit 1
             */
			
			
			//string identifier, int maxHealth, int speed, int strength, string name, List<Attack> attacks, ElementType type, int catchChance, List<Item> drops, int xpAmount
			Attack gust = new Attack("Gust", Enums.ElementType.WIND, 3, 5, 2, null, null, level);
			Attack stonefist = new Attack("Slam!", Enums.ElementType.NORMAL, 2, 5, 4, new StunEffect(20), null, level);
			Attack wingslam = new Attack("Wingslam", Enums.ElementType.WIND, 3, 5, 6, null, null, level);
			Attack crush = new Attack("YOU WILL BE CRUSHED!", Enums.ElementType.EARTH, 5, 7, 10, new StunEffect(30), null, level);
			List<Attack> attacks = new List<Attack> ();
			attacks.Add (gust);
			attacks.Add (stonefist);
			attacks.Add (wingslam);
			attacks.Add (crush);
			
			String name = rand.Next(0, 5) == 0 ? "Goliath" : "Gargoyle";
			
			return new FightViech(level * 10 * 3, level, level*2, name, attacks, Enums.ElementType.EARTH, 50, null, level * 50, "Gargoyles", "GargoyleIcon");

        }

        public FightViech createPanther(int level)
        {
			/* INFO: Base stats:
             * maxHealth = level *10
             * speed = level;
             * strength = level;
             * 
             * Einer dieser stats wird mit 3 multipliziert, einer mit 2 und einer mit 1
             */
			
			
			//string identifier, int maxHealth, int speed, int strength, string name, List<Attack> attacks, ElementType type, int catchChance, List<Item> drops, int xpAmount
			Attack stomp = new Attack("Get stomped!", Enums.ElementType.NORMAL, 2, 4, 2, null, null, level);
			Attack bodyslam = new Attack("Bodyslam", Enums.ElementType.NORMAL, 2, 5, 4, null, null, level);
			Attack flametongue = new Attack("By fire be purged", Enums.ElementType.FIRE, 3, 5, 7, new BurnEffect(50), null, level);
			Attack firebreath = new Attack("BURN MORTAL!", Enums.ElementType.FIRE, 5, 7, 10, new BurnEffect(80), null, level);
			List<Attack> attacks = new List<Attack> ();
			attacks.Add (stomp);
			attacks.Add (flametongue);
			attacks.Add (bodyslam);
			attacks.Add (firebreath);
			
			String name = rand.Next(0, 5) == 0 ? "Protector" : "Panther";
			
			return new FightViech(level * 10 * 3, level * 2, level, name, attacks, Enums.ElementType.FIRE, 50, null, level * 50, "SteirischerPanther", "PantherIcon");

        }

        public FightViech createAlraune(int level)
        {
            /* INFO: Base stats:
             * maxHealth = level *10
             * speed = level;
             * strength = level;
             * 
             * Einer dieser stats wird mit 3 multipliziert, einer mit 2 und einer mit 1
             */

            //string identifier, int maxHealth, int speed, int strength, string name, List<Attack> attacks, ElementType type, int catchChance, List<Item> drops, int xpAmount
            Attack scream = new Attack("Scream", Enums.ElementType.NORMAL, 2, 5, 7, new StunEffect(70), null, level);
            Attack root = new Attack("The root of evil", Enums.ElementType.EARTH, 3, 5, 3, null, null, level);
            Attack whining = new Attack("Unbearable Whining", Enums.ElementType.NORMAL, 5, 7, 5, null, null, level);
            Attack beam = new Attack("Poison seeds", Enums.ElementType.EARTH, 2, 4, 6, new PoisonEffect(50), null, level);
			List<Attack> attacks = new List<Attack> ();
			attacks.Add (scream);
			attacks.Add (root);
			attacks.Add (whining);
			attacks.Add (beam);

            String name = rand.Next(0, 5) == 0 ? "Root Vegetable" : "Alraune";

            return new FightViech(level * 10 * 3, level * 2, level, name, attacks, Enums.ElementType.EARTH, 50, null, level * 50, "Alraune", "AlrauneIcon");
        }

        public FightViech createZerberwelpe(int level)
        {
            /* INFO: Base stats:
             * maxHealth = level * 10
             * speed = level;
             * strength = level;
             * 
             * Einer dieser stats wird mit 3 multipliziert, einer mit 2 und einer mit 1
             */

            //string identifier, int maxHealth, int speed, int strength, string name, List<Attack> attacks, ElementType type, int catchChance, List<Item> drops, int xpAmount
            List<Attack> attacks = new List<Attack>();
            attacks.Add(new Attack("Evil Bark", Enums.ElementType.NORMAL, 2, 5, 6, new StunEffect(70), null, level));
            attacks.Add(new Attack("Razor sharp Teeth", Enums.ElementType.NORMAL, 3, 5, 3, null, null, level));
            attacks.Add(new Attack("Trembling Ground", Enums.ElementType.EARTH, 1, 7, 5, null, null, level));
            attacks.Add(new Attack("Burning Breath", Enums.ElementType.FIRE, 2, 8, 7, new BurnEffect(60), null, level));

            String name = rand.Next(0, 6) == 0 ? "Lassie" : rand.Next(0, 3) == 0 ? "Nasty little Cerberus" : "Zerber Puppy";

            return new FightViech(level * 10, level * 3, level * 2, name, attacks, Enums.ElementType.FIRE, 50, null, level * 50, "Zerberwelpe", "ZerberwelpeIcon");
        }

        public FightViech createSirene(int level)
        {
            /* INFO: Base stats:
             * maxHealth = level * 10
             * speed = level;
             * strength = level;
             * 
             * Einer dieser stats wird mit 3 multipliziert, einer mit 2 und einer mit 1
             */

            //string identifier, int maxHealth, int speed, int strength, string name, List<Attack> attacks, ElementType type, int catchChance, List<Item> drops, int xpAmount
            List<Attack> attacks = new List<Attack>();
            attacks.Add(new Attack("Wet Claws", Enums.ElementType.NORMAL, 2, 5, 2, null, null, level));
            attacks.Add(new Attack("Watery Grave", Enums.ElementType.NORMAL, 3, 5, 3, null, null, level));
            attacks.Add(new Attack("Drowning", Enums.ElementType.WATER, 5, 8, 6, new FreezeEffect(50), null, level));
            attacks.Add(new Attack("Song of the Sirens", Enums.ElementType.WIND, 2, 4, 5, new SleepEffect(50), null, level));

            String name = rand.Next(0, 6) == 0 ? "Arielle" : "Siren";

            return new FightViech(level * 10 * 2, level * 3, level, name, attacks, Enums.ElementType.WATER, 50, null, level * 50, "Siren", "SirenIcon");
        }
    }
}
