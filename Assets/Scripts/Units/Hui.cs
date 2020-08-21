using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HuiStage { Psychic, Flying, Ground}

public class Hui : EnemyUnit
{
    HuiStage Stage;

    public List<EnemySkill> PsychicSkills;
    public List<EnemySkill> FlyingSkills;
    public List<EnemySkill> GroundSkills;

    protected override void Awake()
    {
        Stage = HuiStage.Flying;

        Speed = new Stat(20);
        Defense = new Stat(20);
        Attack = new Stat(20);
        Luck = new Stat(20);

        base.Awake();

        FlyingSkills = new List<EnemySkill>();
        GroundSkills = new List<EnemySkill>();
        
        FlyingSkills.Add(new Tornado(this));
        FlyingSkills.Add(new PoisonousSpit(this));

        GroundSkills.Add(new Sweep(this));
        GroundSkills.Add(new Constrict(this));
        GroundSkills.Add(new VenomousBite(this));
    }


    public override void PerformAction()
    {
        //CHANGE STAGE HERE
        if(CurrentHP < MaxHP / 2)
        {
            Stage = HuiStage.Ground;
        }

        switch (Stage)
        {
            case HuiStage.Psychic:
                PerformSkill(PsychicSkills[Random.Range(0, PsychicSkills.Count)]);
                //PsychicConstrict
                //Hypnotize
                //Confusion
                break;
            case HuiStage.Flying:
                PerformSkill(FlyingSkills[Random.Range(0, FlyingSkills.Count)]);
                //Tornado
                //PoisonSpit
                break;
            case HuiStage.Ground:
                PerformSkill(GroundSkills[Random.Range(0, GroundSkills.Count)]);
                //Sweep
                //Constrict
                //VenomousBite
                break;
        }
    }

    

}
