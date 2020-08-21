using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CharacterUnit : Unit
{
    public  List<Skill> Skills;
    

    public List<Skill> GetSkills()
    {
        return Skills;
    }

    protected override void Awake()
    {
        UnitType = UnitType.Friendly;
        Skills = new List<Skill>();
        base.Awake();
    }
}
