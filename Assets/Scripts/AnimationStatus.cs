using UnityEngine;
using System.Collections;
using Assets.Scripts;
using Assets.Scripts.Utils;

public class AnimationStatus : MonoBehaviour 
{
    public Enums.MonsterStatus Status;
    public Effect.EffectType SpecialDamageStatus;

    private Animator MonsterAnimator;

	// Use this for initialization
	public void Start () 
    {
	    MonsterAnimator = GetComponent<Animator>();
	}

    public void PlaySpecialDamageEffect(Effect.EffectType effectType)
    {
        if (Status == Enums.MonsterStatus.IsIdle)
        {
            SpecialDamageStatus = effectType;
            Status = Enums.MonsterStatus.IsSpecial;
            switch (effectType)
            {
                case Effect.EffectType.NONE:
                    MonsterAnimator.SetTrigger("Hurt");
                    break;
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
        if (Status == Enums.MonsterStatus.IsIdle)
        {
            Status = Enums.MonsterStatus.IsSpecial;
            MonsterAnimator.SetBool("Sleeping", true);
        }
    }

    public void WakeUp()
    {
        if (Status == Enums.MonsterStatus.IsSleeping)
        {
            MonsterAnimator.SetBool("Sleeping", false);
        }
    }

    public void Die()
    {
        Status = Enums.MonsterStatus.IsDead;
        MonsterAnimator.SetTrigger("Death");
    }

    public bool areSpechialAnimationsFinished()
    {
        return Status == Enums.MonsterStatus.IsSleeping || Status == Enums.MonsterStatus.IsIdle || Status == Enums.MonsterStatus.IsDead;
    }

    public Enums.MonsterStatus GetStatus()
    {
        return Status;
    }

    /**
     * Do not use this function!!!!
     * Do not delete this function!!!!
     */
    public void SetIdleState()
    {
        Status = Enums.MonsterStatus.IsIdle;
    }

    public void SetDeadState()
    {
        Status = Enums.MonsterStatus.IsDead;
    }
}
