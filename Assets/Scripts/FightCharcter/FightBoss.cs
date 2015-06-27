using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Assets.Scripts.Utils;
using Assets.Scripts.Consumables;

namespace Assets.Scripts.FightCharacters
{
    class FightBoss : FightViech
    {
       private List<FightViech> activeViecher;

       public FightBoss(int maxHealth, int speed, int strength, string name, List<Attack> attacks, Enums.ElementType type, int catchChance, List<Item> drops, int xpAmount, List<FightViech> activeViecher, GameObject sprite, Sprite icon)
           : base(maxHealth, speed, strength, name, attacks, type, catchChance, drops, xpAmount, sprite, icon)
        {
            this.activeViecher = activeViecher;
        }

        public List<FightViech> ActiveViecher
        {
            get { return activeViecher; }
        }
    }
}
