﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralyze : ActionSelectionEffect
{
    public override bool PerformEffect(Unit u)
    {
        return UnityEngine.Random.value < 0.5f;
    }

    public Paralyze() : this(3) { }

    public Paralyze(int Turns)
    {
        Stackable = false;
        TurnDuration = Turns;
    }
}
