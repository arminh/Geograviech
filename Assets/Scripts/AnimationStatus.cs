using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;

public class AnimationStatus : MonoBehaviour 
{
    public Enums.MonsterStatus Status;

    private Animator MonsterAnimator;

	// Use this for initialization
	public void Start () 
    {
	    MonsterAnimator = GetComponent<Animator>();
	}

    public bool SetOnFire 
    { 
        get 
        {
            return Status == Enums.MonsterStatus.IsBurning;
        }
        set
        {
            SetStatus(Enums.MonsterStatus.IsBurning, value);
        }
    }
    public bool SetFrozen
    {
        get
        {
            return Status == Enums.MonsterStatus.IsFrozen;
        }
        set
        {
            SetStatus(Enums.MonsterStatus.IsFrozen, value);
        }
    }
    public bool SetSleeping
    {
        get
        {
            return Status == Enums.MonsterStatus.IsSleeping;
        }
        set
        {
            SetStatus(Enums.MonsterStatus.IsSleeping, value);
        }
    }
    public bool SetPoisoned
    {
        get
        {
            return Status == Enums.MonsterStatus.IsPoisoned;
        }
        set
        {
            SetStatus(Enums.MonsterStatus.IsPoisoned, value);
        }
    }
    public bool SetStunned
    {
        get
        {
            return Status == Enums.MonsterStatus.IsStunned;
        }
        set
        {
            SetStatus(Enums.MonsterStatus.IsStunned, value);
        }
    }

    public void SetIdle()
    {
        Status = Enums.MonsterStatus.IsIdle;
    }

    public void SetDead()
    {
        Status = Enums.MonsterStatus.IsDead;
        MonsterAnimator.SetTrigger("Death");
    }

    public void WasHurt()
    {
        MonsterAnimator.SetTrigger("Hurt");
    }

    private void SetStatus(Enums.MonsterStatus stat, bool value)
    {
        if (value && Status == Enums.MonsterStatus.IsIdle)
        {
            Status = stat;
            SetAnimationBool(stat, true);
        }
        if (!value)
        {
            SetAnimationBool(stat, false);
        }
    }

    public Enums.MonsterStatus GetStatus()
    {
        return Status;
    }


    private void SetAnimationBool(Enums.MonsterStatus stat, bool value)
    {
        switch (stat)
        {
            case Enums.MonsterStatus.IsSleeping:
                MonsterAnimator.SetBool("Sleeping", value);
                break;
            case Enums.MonsterStatus.IsBurning:
                MonsterAnimator.SetBool("Burning", value);
                break;
            case Enums.MonsterStatus.IsStunned:
                MonsterAnimator.SetBool("Stunned", value);
                break;
            case Enums.MonsterStatus.IsPoisoned:
                MonsterAnimator.SetBool("Poisoned", value);
                break;
            case Enums.MonsterStatus.IsFrozen:
                MonsterAnimator.SetBool("Frozen", value);
                break;
        }
    }
}
