using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HuiStage { Psychic, Flying, Ground}

public class Hui : Unit, IEnemy
{
    HuiStage Stage;

    public List<EnemySkill> PsychicSkills;
    public List<EnemySkill> FlyingSkills;
    public List<EnemySkill> GroundSkills;

    void Start()
    {
        Stage = HuiStage.Flying;
    }


    public void PerformAction()
    {
        //CHANGE STAGE HERE
        if(CurrentHP < MaxHP / 2)
        {
            Stage = HuiStage.Ground;
        }

        switch (Stage)
        {
            case HuiStage.Psychic:
                PsychicSkills[Random.Range(0, PsychicSkills.Count)].PerformSkill();
                //PsychicConstrict
                //Hypnotize
                //Confusion
                break;
            case HuiStage.Flying:
                FlyingSkills[Random.Range(0, FlyingSkills.Count)].PerformSkill();
                //Hurricane
                //PoisonSpit
                break;
            case HuiStage.Ground:
                GroundSkills[Random.Range(0, GroundSkills.Count)].PerformSkill();
                //Sweep
                //Constrict
                //Bite
                break;
        }
    }

    

}
