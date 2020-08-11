using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EndOfTurnEffect : SkillEffect
{
    public abstract void PerformEffect(Unit u);
}
