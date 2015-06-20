using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.Consumables;

namespace Assets.Scripts
{
    class FightBoss : FightViech
    {
       private List<FightViech> activeViecher;

       public FightBoss(int maxHealth, int speed, int strength, string name, List<Attack> attacks, ElementType type, int catchChance, List<Item> drops, int xpAmount, List<FightViech> activeViecher, GameObject sprite, GameObject icon)
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
