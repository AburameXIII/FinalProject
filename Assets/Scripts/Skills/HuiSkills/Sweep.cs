using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweep: EnemySkill
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

    public Sweep(Unit User): base(User)
    {
        MinAttackMultiplier = 0.7f;
        MaxAttackMultiplier = 0.8f;
        SkillName = "Sweep";
    }


    public override IEnumerator Performing()
    {
        Targets = CombatManager.Instance.Characters;

        yield return new WaitForSeconds(0.5f);

        Perform();

        //Go to end of turn actions
        User.EndOfTurn();
    }

}
