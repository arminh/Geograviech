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
        public List<GameObject> allCharactersPefabs;
        private Dictionary<string, GameObject> prefabs;

        public List<Sprite> allCharactersIcons;
        private Dictionary<string, Sprite> icons;

        public FightViechFactory()
        {
            foreach (GameObject sprite in allCharactersPefabs)
            {
                prefabs.Add(sprite.name, sprite);
            }

            foreach (Sprite icon in allCharactersIcons)
            {
                icons.Add(icon.name, icon);
            }
        }

        public FightViech createFightViech(Enums.ElementType type, int level)
        {
            return null;
        }

        private FightViech createAlraune(int level)
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

            return new FightViech(level * 10 * 3, level * 2, level, "Alraune", null, Enums.ElementType.EARTH, 50, null, level * 50, "Alraune", "AlrauneIcon");
        }
    }
}
