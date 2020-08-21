using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseAttack : PersitantEffect
{
    private StatModifier AttackModifier;

    public IncreaseAttack(float Percentage)
    {
        AttackModifier = new StatModifier(Percentage, StatModType.Percentage);
        Effect = Effect.AttackUp;
        Stackable = true;
    }

    public override void PerformEffect(Unit u)
    {
        u.Attack.AddModifier(AttackModifier);
    }

    public override void UndoEffect(Unit u)
    {
        u.Attack.RemoveModifier(AttackModifier);
    }

}
