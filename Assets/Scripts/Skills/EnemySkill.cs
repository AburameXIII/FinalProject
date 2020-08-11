using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySkill : Skill
{
    public override bool CanPerform()
    {
        return true;
    }

    public abstract void PerformSkill();

    
}
