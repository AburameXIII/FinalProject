using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constrict : EnemySkill
{
    public Paralyze Paralyze;

    public override void Perform()
    {
        foreach (Unit t in Targets)
        {
            t.AddAfterActionSelectionEffect(Paralyze);
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
        //Chooses random target
        PerformSkill(new List<Unit>() { CombatManager.Instance.Characters[Random.Range(0,CombatManager.Instance.Characters.Count)] });
    }

    public override void PerformSkill(List<Unit> Targets)
    {
        this.Targets = Targets;
        StartCoroutine(Performing());
    }
}
