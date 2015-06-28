using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Utils
{
    public class Enums
    {
        public enum MonsterStatus
        {
            IsIdle,
            IsSleeping,
            IsDead,
            IsSpecial
        };

        public enum ElementType 
        { 
            EARTH, 
            WIND, 
            FIRE, 
            WATER, 
            NORMAL 
        };

        public enum MonsterType
        {
            Water,
            Fire,
            Earth,
            Air
        };


        public enum LogBookEntryType
        {
            INFO = 0,
            ERROR = 1
        };

        public enum ItemType
        {
            Weapon,
            Monster,
            Consumable
        }
    }
}
