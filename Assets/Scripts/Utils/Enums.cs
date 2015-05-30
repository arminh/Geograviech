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
            IsBurning,
            IsStunned,
            IsPoisoned,
            IsFrozen,
            IsDead
        };

        public enum MonsterType
        {
            Water,
            Fire,
            Earth,
            Air
        };

        public enum AttackType
        {
            Normal,
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
    }
}
