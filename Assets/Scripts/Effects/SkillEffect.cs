using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType { BeforeActionSelection, AfterActionSelection, Persitant, AfterActionCompletion}
public enum Effect { AttackUp, Poison, Paralyze}

public abstract class SkillEffect : MonoBehaviour
{
    public Effect Effect;
    public bool Stackable;
    public int TurnDuration;
    public Sprite SkillSprite;
    public void DecreaseDuration()
    {
        TurnDuration--;
    }

    public override bool Equals(object other)
    {
        SkillEffect otherSkillEffect = other as SkillEffect;
        if (otherSkillEffect == null) return false;
        else return otherSkillEffect.Effect.Equals(Effect);
    }

    public override int GetHashCode()
    {
        return Effect.GetHashCode();
    }
}
