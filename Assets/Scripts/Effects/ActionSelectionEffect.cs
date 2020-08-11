using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionSelectionEffect : SkillEffect
{
    public abstract bool PerformEffect(Unit u);
}
