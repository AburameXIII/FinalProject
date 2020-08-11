using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralyze : ActionSelectionEffect
{
    public override bool PerformEffect(Unit u)
    {
        return UnityEngine.Random.value < 0.5f;
    }

    void Awake()
    {
        Effect = Effect.Paralyze;
        Stackable = false;
    }
}
