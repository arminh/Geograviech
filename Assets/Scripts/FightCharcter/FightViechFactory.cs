using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class FightViechFactory: MonoBehaviour
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

        public FightViech createFightViech(ElementType type, int level)
        {
            return null;
        }

        private FightViech createAlraune(int level)
        {
            //string identifier, int maxHealth, int speed, int strength, string name, List<Attack> attacks, ElementType type, int catchChance, List<Item> drops, int xpAmount
            Attack scream = new Attack("Scream", ElementType.NORMAL, level * 3, new StunEffect(70), null);
           // Attack halluzinate = new Attack("Halluzinate", ElementType.NORMAL, )

            return new FightViech(level * 10 * 3, level * 2, level, "Alraune", null, ElementType.EARTH, 50, null, level * 50, prefabs["Alraune"], icons["AlrauneIcon"]);
        }
    }
}
