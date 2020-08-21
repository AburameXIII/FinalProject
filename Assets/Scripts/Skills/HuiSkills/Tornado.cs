using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : EnemySkill
{

    private float MinAttackMultiplier;
    private float MaxAttackMultiplier;
    public override void Perform()
    {
        foreach (Unit t in Targets)
        {
            t.TakeDamage(MinAttackMultiplier, MaxAttackMultiplier, User);
        }
    }

    public Tornado(Unit User) : base(User)
    {
        MinAttackMultiplier = 0.3f;
        MaxAttackMultiplier = 0.4f;
        SkillName = "Tornado";
    }

    public override IEnumerator Performing()
    {
        Targets = CombatManager.Instance.Characters;


        yield return new WaitForSeconds(0.5f);

        

        for (int i = 0; i < 3; i++)
        {
            Perform();
            yield return new WaitForSeconds(0.2f);
        }

        //Go to end of turn actions
        User.EndOfTurn();
    }



    
}
