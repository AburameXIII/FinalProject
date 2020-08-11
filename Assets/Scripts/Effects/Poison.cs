using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : EndOfTurnEffect
{
    public override void PerformEffect(Unit u)
    {
        u.ChangeHealth(-Mathf.RoundToInt(u.MaxHP * 0.05f));
    }

    void Awake()
    {
        Effect = Effect.Poison;
        Stackable = true;
    }

}
