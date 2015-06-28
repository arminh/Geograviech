using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

using Assets.Scripts.FightCharacters;

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
                    //DontDestroyOnLoad(instance.gameObject);
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
            //TODO: CD auf Spezialattacken warad vl net so schlecht!!!!

            List<FightCharacter> attackAble = FightManager.Instance.getPlayerViecher(true);
            var attacks = viech.Attacks;
            Attack chosenAttack = null;
            FightCharacter chosenViech = null;

            chosenViech = getAttackableViechWithTheMostLife(attackAble);
            if (chosenViech.CurrentEffect != null && !chosenViech.CurrentEffect.IsCCType)
            {
                if (viechHasCCAttack(attacks))
                {
                    //TODO: falls ein viech mehr als 1 cc attacke hat muss man noch eine priosisierung einbauen
                    chosenAttack = attacks.Where(y => y.Effect != null).FirstOrDefault(x => x.Effect.IsCCType);
                }
                else
                {
                    chosenViech = getAttackableViechWithTheLowestLife(attackAble);
                    if (viechHasSDAttack(attacks))
                    {
                        chosenAttack = attacks.Where(y => y.Effect != null).FirstOrDefault(x => x.Effect.IsSDType);
                    }
                    else
                    {
                        chosenAttack = attacks.FirstOrDefault(x => x.Effect == null);
                    }
                }
            }
            else
            {
                chosenViech = getAttackableViechWithTheLowestLife(attackAble);

                if (viechHasSDAttack(attacks))
                {
                    //TODO: falls ein viech mehr als 1 sd attacke hat muss man noch eine priosisierung einbauen
                    chosenAttack = attacks.Where(y => y.Effect != null).FirstOrDefault(x => x.Effect.IsSDType);
                }
                else
                {
                    chosenAttack = attacks.FirstOrDefault(x => x.Effect == null);
                }
            }


            return new KeyValuePair<Attack, FightCharacter>(chosenAttack, chosenViech);
        }


        private bool viechHasCCAttack(List<Attack> attacks)
        {
            var result = attacks.Where(y => y.Effect != null).Any(x => x.Effect.IsCCType);
            return result;
        }

        private bool viechHasSDAttack(List<Attack> attacks)
        {
            var result = attacks.Where(y => y.Effect != null).Any(x => x.Effect.IsSDType);
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

