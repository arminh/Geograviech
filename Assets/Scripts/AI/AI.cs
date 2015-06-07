using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Assets.Scripts.ArtificialIntelligence
{
    public static class AI 
    {

        public static void executeTurn(FightCharacter viech)
        {

        }

        private static void selectAttack(Action<String> function, Dictionary<String, int> items)
        {
            string item = items.FirstOrDefault().Key;

            function.Invoke(item);
        }

        private static void selectAttackEnemy(Attack attack)
        {
            List<FightCharacter> attackAble = FightManager.Instance.getPlayerViecher(true);

            FightManager.Instance.attackViech(attack, attackAble.FirstOrDefault());
        }
    }
}

