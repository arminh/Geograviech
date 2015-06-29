using UnityEngine;
using System.Collections;

using Assets.Scripts;
using Assets.Scripts.Utils;
using Assets.Scripts.Effects;

public class AnimationStatus : MonoBehaviour 
{
    public Enums.MonsterStatus Status;
    public Effect.EffectType SpecialDamageStatus;
    public int TriggerCount;

    private Animator MonsterAnimator;


	// Use this for initialization
	public void Start () 
    {
	    MonsterAnimator = GetComponent<Animator>();
        Status = Enums.MonsterStatus.IsIdle;
        SpecialDamageStatus = Effect.EffectType.NONE;
        TriggerCount = 0;
	}

    public void PlayNormalDamageEffect()
    {
        if (Status != Enums.MonsterStatus.IsSleeping && Status != Enums.MonsterStatus.IsDead)
        {
            Status = Enums.MonsterStatus.IsSpecial;
            TriggerCount++;
            MonsterAnimator.SetTrigger("Hurt");
        }
    }

    public void PlaySpecialDamageEffect(Effect.EffectType effectType)
    {
        if (Status != Enums.MonsterStatus.IsSleeping && Status != Enums.MonsterStatus.IsDead && effectType != Effect.EffectType.SLEEP)
        {
            SpecialDamageStatus = effectType;
            Status = Enums.MonsterStatus.IsSpecial;
            TriggerCount++;
            switch (effectType)
            {
                case Effect.EffectType.BURN:
                    MonsterAnimator.SetTrigger("Burning");
					break;
                case Effect.EffectType.STUN:
                    MonsterAnimator.SetTrigger("Stunned");
                    break;
                case Effect.EffectType.POISON:
                    MonsterAnimator.SetTrigger("Poisoned");
                    break;
                case Effect.EffectType.FREEZE:
                    MonsterAnimator.SetTrigger("Frozen");
                    break;
            }
        }
    }

    public void PlaySpecialDamageEffectAgain()
    {
        if (SpecialDamageStatus != Effect.EffectType.NONE)
            PlaySpecialDamageEffect(SpecialDamageStatus);
    }

    public void ResetSpecialDamageEffect()
    {
        SpecialDamageStatus = Effect.EffectType.NONE;
    }

    public void FallAsleep()
    {
        Status = Enums.MonsterStatus.IsSpecial;
        TriggerCount++;
        MonsterAnimator.SetBool("Sleeping", true);
    }

    public void WakeUp()
    {
        Status = Enums.MonsterStatus.IsSpecial;
        TriggerCount++;
        MonsterAnimator.SetBool("Sleeping", false);
    }

    public void Die()
    {
        Status = Enums.MonsterStatus.IsSpecial;
        TriggerCount++;
        MonsterAnimator.SetTrigger("Death");
    }

    public void Attack()
    {
        Status = Enums.MonsterStatus.IsSpecial;
        TriggerCount++;
        MonsterAnimator.SetTrigger("Attack");
    }

    public bool areSpechialAnimationsFinished()
    {
        return (Status == Enums.MonsterStatus.IsSleeping || Status == Enums.MonsterStatus.IsIdle || Status == Enums.MonsterStatus.IsDead) && (TriggerCount == 0);
    }

    public Enums.MonsterStatus GetStatus()
    {
        return Status;
    }

    /**
     * Do not use this function!!!!
     * Do not delete this function!!!!
     */
    public void SetState(Enums.MonsterStatus state)
    {  
        if (Status == Enums.MonsterStatus.IsSpecial)
        {
            Debug.Log(string.Format("Animation Callback FROM: {0} {1}", Status, TriggerCount));
            TriggerCount--;
            Status = TriggerCount > 0 ? Status : state;
            Debug.Log(string.Format("Animation Callback TO: {0} {1}", Status, TriggerCount));
        }   
    }
}
