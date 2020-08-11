using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public string SkillName;
    public Sprite ImageSkill;
    public string SkillDescription;
    public Unit u;
    protected List<Unit> Targets;

    public abstract bool CanPerform();

    public abstract void PerformSkill(List<Unit> Targets);
    public abstract void Perform();

    public abstract IEnumerator Performing();
}
