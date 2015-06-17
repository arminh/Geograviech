using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Assets.Scripts.ArtificialIntelligence
{
    public class AI : MonoBehaviour
    {
        private static AI instance;

        public static AI Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = GameObject.FindObjectOfType<AI>();

                    //Tell unity not to destroy this object when loading a new scene!
                    DontDestroyOnLoad(instance.gameObject);
                }
                return instance;
            }
        }


        public IEnumerator executeTurn(FightCharacter viech)
        {
            Attack attack = viech.Attacks.FirstOrDefault();

            return selectAttackEnemy(attack);
        }

        private void selectAttack(Action<String> function, Dictionary<String, int> items)
        {
            string item = items.FirstOrDefault().Key;

            function.Invoke(item);
        }

        private IEnumerator selectAttackEnemy(Attack attack)
        {
            List<FightCharacter> attackAble = FightManager.Instance.getPlayerViecher(true);
            return FightManager.Instance.attackViech(attack, attackAble.FirstOrDefault());
        }


    }
}

