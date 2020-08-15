using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milo : Unit, ICharacter
{
    public Character MiloCharacter;
    public List<Skill> Skills;

    public List<Skill> GetSkills()
    {
        return Skills;
    }

    public void PerformSkill(int i, List<Unit> Targets)
    {
        foreach (Pair<ActionSelectionEffect,int> e in AfterActionSelectionEffects)
        {
            if (e.First.PerformEffect(this))
            {
                //DID NOT PERFORM SKILL
                EndOfTurn();
                return;
            }
            
            
        }

        Skills[i].PerformSkill(Targets);
    }

    protected override void Awake()
    {
       
        UnitName = "Milo";
        MaxHP = 900;
        CurrentHP = 900;

        MaxSecondaryResource = 100;
        CurrentSecondaryResource = 0;
        SecondaryResource = SecondaryResourceType.RG;

        BaseSpeed = 200;
        BaseDefense = 100;
        BaseAttack = 999;

        CurrentSpeed = 200;
        CurrentDefense = 200;
        CurrentAttack = 999;

        TurnSprite = MiloCharacter.CharacterProfilePicture;

        base.Awake();
    }

   

}
