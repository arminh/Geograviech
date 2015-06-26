using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Scripts.Utils;

namespace Assets.Scripts
{
    public abstract class Effect
    {
        protected EffectType type;
        protected int inflictChance;
        protected int cureChance;

        protected string inflictMsg;

        public Effect(int inflictChance, int cureChance, EffectType type)
        {
            this.inflictChance = inflictChance;
            this.type = type;
        }

        protected System.Random rand = new System.Random();

        public virtual void inflict(FightCharacter character)
        {
            if (character.CurrentEffect == null)
            {
                int num = rand.Next(1, 100);

                if (num <= inflictChance)
                {
                    Log.Instance.Info(character.Name + " is now " + inflictMsg + ".");
                    character.CurrentEffect = this;
                    character.Sprite.GetComponentInChildren<AnimationStatus>().PlaySpecialDamageEffect(this.type);
                }
            }  
        }

        protected bool tryCure(FightCharacter character)
        {
            int curePercentage = rand.Next(1, 100);

            if (curePercentage <= cureChance)
            {
                cure(character);
                return true;
            }
            else
            {
                Log.Instance.Info(character.Name + " is still " + inflictMsg + ".");
                return false;
            }
        }

        public virtual void cure(FightCharacter character)
        {
            Log.Instance.Info(character.Name + " is no longer " + inflictMsg + ".");
            character.CurrentEffect = null;
            character.Sprite.GetComponentInChildren<AnimationStatus>().ResetSpecialDamageEffect();
        }
        public abstract IEnumerator execute(FightCharacter character);

        public enum EffectType { BURN, POISON, SLEEP, STUN, FREEZE, NONE };

        protected void increaseCureChance(int value)
        {
            cureChance += value;
        }

        public EffectType Type
        {
            get { return type;  }
        }

        public int InflictChance
        {
            get { return inflictChance; }
        }

        public string InflictMsg
        {
            get { return inflictMsg; }
        }

        // is crowdcontrol type
        public bool IsCCType
        {
            get
            {
                return type == EffectType.STUN || type == EffectType.SLEEP || type == EffectType.FREEZE;
            }
        }

        // is special damage type
        public bool IsSDType
        {
            get
            {
                return type == EffectType.BURN || type == EffectType.POISON;
            }
        }
    }
}
