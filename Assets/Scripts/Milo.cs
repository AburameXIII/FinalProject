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
        int Level = PartyManager.Instance.GetTrainProgression(MiloCharacter).GetLevel();

        MaxHP = MiloCharacter.HP[Level-1];
        CurrentHP = MiloCharacter.HP[Level - 1];

        MaxSecondaryResource = 100;
        CurrentSecondaryResource = 0;
        SecondaryResource = SecondaryResourceType.RG;

        BaseSpeed = MiloCharacter.Speed[Level-1];
        BaseDefense = MiloCharacter.Defense[Level - 1];
        BaseAttack = MiloCharacter.Attack[Level - 1];
        BaseLuck = MiloCharacter.Luck[Level - 1];

        CurrentSpeed = MiloCharacter.Speed[Level - 1];
        CurrentDefense = MiloCharacter.Defense[Level - 1];
        CurrentAttack = MiloCharacter.Attack[Level - 1];
        CurrentLuck = MiloCharacter.Luck[Level - 1];

        TurnSprite = MiloCharacter.CharacterProfilePicture;

        base.Awake();
    }

   

}
