using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;

public class AnimationStatus : MonoBehaviour 
{
    public Enums.MonsterStatus Status;
    public Enums.MonsterAttackEffect SpecialDamageStatus;

    private Animator MonsterAnimator;

	// Use this for initialization
	public void Start () 
    {
	    MonsterAnimator = GetComponent<Animator>();
	}

    public void PlaySpecialDamageEffect(Enums.MonsterAttackEffect effect)
    {
        if (Status == Enums.MonsterStatus.IsIdle)
        {
            SpecialDamageStatus = effect;
            Status = Enums.MonsterStatus.IsSpecial;
            switch (effect)
            {
                case Enums.MonsterAttackEffect.None:
                    MonsterAnimator.SetTrigger("Hurt");
                    break;
                case Enums.MonsterAttackEffect.Burning:
                    MonsterAnimator.SetTrigger("Burning");
                    break;
                case Enums.MonsterAttackEffect.Stunned:
                    MonsterAnimator.SetTrigger("Stunned");
                    break;
                case Enums.MonsterAttackEffect.Poisoned:
                    MonsterAnimator.SetTrigger("Poisoned");
                    break;
                case Enums.MonsterAttackEffect.Frozen:
                    MonsterAnimator.SetTrigger("Frozen");
                    break;
            }
        }
    }

    public void PlaySpecialDamageEffectAgain()
    {
        if (SpecialDamageStatus != Enums.MonsterAttackEffect.None)
            PlaySpecialDamageEffect(SpecialDamageStatus);
    }

    public void ResetSpecialDamageEffect()
    {
        SpecialDamageStatus = Enums.MonsterAttackEffect.None;
    }

    public void FallAsleeping()
    {
        if (Status == Enums.MonsterStatus.IsIdle)
        {
            Status = Enums.MonsterStatus.IsSleeping;
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
        return false;
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
}
