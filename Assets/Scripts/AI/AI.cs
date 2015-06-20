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
            var attackEnemy = selectEnemyToAttack(viech);


            return FightManager.Instance.attackViech(attackEnemy.Key, attackEnemy.Value);
        }

        private KeyValuePair<Attack, FightCharacter> selectEnemyToAttack(FightCharacter viech)
        {


            var attacks = viech.Attacks;
            var attack = selectAttack(attacks);
            List<FightCharacter> attackAble = FightManager.Instance.getPlayerViecher(true);


            return new KeyValuePair<Attack, FightCharacter>(attack, attackAble.FirstOrDefault());
        }


        private Attack selectAttack(List<Attack> attacks)
        {
            List<Attack> activeAttacks = attacks.Where(x => x.Active == true).ToList<Attack>();
            var attack = activeAttacks.FirstOrDefault();
            return attack;
        }


        private bool viechHasCCAttack(FightCharacter viech)
        {
            var result = viech.Attacks.Any(x => x.Effect.Type == Effect.EffectType.FREEZE ||
                                                x.Effect.Type == Effect.EffectType.SLEEP ||
                                                x.Effect.Type == Effect.EffectType.STUN);
            return result;
        }

        private bool viechHasSDAttack(FightCharacter viech)
        {
            var result = viech.Attacks.Any(x => x.Effect.Type == Effect.EffectType.BURN ||
                                                x.Effect.Type == Effect.EffectType.POISON);
            return result;
        }

        private FightCharacter getAttackableViechWithTheMostLife(List<FightCharacter> viecher)
        {
            return viecher.Where(y => y.Health == viecher.Max(x => x.Health)).FirstOrDefault();
        }

        private FightCharacter getAttackableViechWithTheLowestLife(List<FightCharacter> viecher)
        {
            return viecher.Where(y => y.Health == viecher.Min(x => x.Health)).LastOrDefault();
        }

    }
}

