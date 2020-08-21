using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : EndOfTurnEffect
{
    private float HealthPercentage;

    public Poison(float HealthPercentage, int Turns)
    {
        this.HealthPercentage = HealthPercentage;
        Stackable = true;
        TurnDuration = Turns;
    }

    public Poison() : this(0.05f, 3) { }

    public override void PerformEffect(Unit u)
    {
        u.ChangeHealth(-Mathf.RoundToInt(u.MaxHP * HealthPercentage));
    }

}
