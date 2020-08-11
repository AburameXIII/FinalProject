using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonousSpit : EnemySkill
{
    public Poison Poison;

    public override void Perform()
    {
        foreach (Unit t in Targets)
        {
            t.AddEndOfTurnEffect(Poison);
        }
    }

    public override IEnumerator Performing()
    {
        yield return new WaitForSeconds(0.5f);

        Perform();

        //Go to end of turn actions
        u.EndOfTurn();
    }

    public override void PerformSkill()
    {
        PerformSkill(CombatManager.Instance.Characters);
    }

    public override void PerformSkill(List<Unit> Targets)
    {
        this.Targets = Targets;
        StartCoroutine(Performing());
    }
}
