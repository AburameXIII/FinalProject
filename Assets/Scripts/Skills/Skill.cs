using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType
{
    Self, SingleEnemy, SingleAlly, AllAllies, AllEnemies, SelfAndSingleAlly, 
}

public abstract class Skill
{
    public string SkillName;
    public string SkillDescription;
    public Unit User;
    protected List<Unit> Targets;
    public TargetType TargetType;
    public Sprite SkillImage;
    
    public Skill(Unit User)
    {
        this.User = User;
    }

    public abstract bool CanPerform();

    public abstract void Perform();

    public abstract IEnumerator Performing();
}
