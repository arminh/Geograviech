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

        public FightViech createFightViech(Enums.ElementType type, int level)
        {
            FightViech monster = null;
            switch (rand.Next(1, 3))
            {
                case 1:
                    monster = createAlraune (GameManager.Instance.getPlayer().Level);
                    break;
                case 2:
                    monster = createZerberwelpe(GameManager.Instance.getPlayer().Level);
                    break;
            }
            return monster;
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
