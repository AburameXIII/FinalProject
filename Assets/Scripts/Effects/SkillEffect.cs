using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillEffect
{
    public bool Stackable;
    protected int TurnDuration;


    public bool DecreaseDuration()
    {
        TurnDuration--;
        return TurnDuration <= 0;
    }


    public override bool Equals(object other)
    {
        SkillEffect otherSkillEffect = other as SkillEffect;
        if (otherSkillEffect == null) return false;
        else return otherSkillEffect.GetType().Equals(GetType());
    }

    public override int GetHashCode()
    {
        return GetType().GetHashCode();
    }
}
