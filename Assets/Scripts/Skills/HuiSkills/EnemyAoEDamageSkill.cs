using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAoEDamageSkill: EnemySkill
{
    public float MinAttackMultiplier;
    public float MaxAttackMultiplier;
    public override void Perform()
    {
        foreach (Unit t in Targets)
        {
            t.TakeDamage(MinAttackMultiplier, MaxAttackMultiplier, u);
            
        }
    }

    public override IEnumerator Performing()
    {
        yield return new WaitForSeconds(0.5f);

        Perform();

        //Go to end of turn actions
        u.EndOfTurn();
    }

    public override void PerformSkill(List<Unit> Targets)
    {
        this.Targets = Targets;
        StartCoroutine(Performing());
    }

    public override void PerformSkill()
    {
        PerformSkill(CombatManager.Instance.Characters);
    }
}
