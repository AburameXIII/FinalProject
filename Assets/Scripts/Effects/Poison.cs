using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : EndOfTurnEffect
{
    private float HealthPercentage;

    public Poison(float HealthPercentage)
    {
        this.HealthPercentage = HealthPercentage;
        this.Effect = Effect.Poison;
        Stackable = true;
    }

    public Poison() : this(0.05f) { }

    public override void PerformEffect(Unit u)
    {
        u.ChangeHealth(-Mathf.RoundToInt(u.MaxHP * HealthPercentage));
    }

}
