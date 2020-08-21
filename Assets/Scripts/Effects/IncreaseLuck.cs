using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseLuck : PersitantEffect
{
    private StatModifier LuckModifier;

    public IncreaseLuck(float Percentage, int Turns)
    {
        LuckModifier = new StatModifier(Percentage, StatModType.Percentage);
        Stackable = true;
        TurnDuration = Turns;
    }

    public override void PerformEffect(Unit u)
    {
        u.Attack.AddModifier(LuckModifier);
    }

    public override void UndoEffect(Unit u)
    {
        u.Attack.RemoveModifier(LuckModifier);
    }

}
