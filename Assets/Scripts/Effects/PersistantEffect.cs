using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PersitantEffect : SkillEffect
{
    public abstract void PerformEffect(Unit u);
    public abstract void UndoEffect(Unit u);
}
