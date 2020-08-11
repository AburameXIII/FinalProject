using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenomousBite : EnemySkill
{
    public float MinAttackMultiplier;
    public float MaxAttackMultiplier;
    public Poison Poison;

    public override void Perform()
    {
        foreach (Unit t in Targets)
        {
            t.TakeDamage(MinAttackMultiplier, MaxAttackMultiplier, u);
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
        //Chooses tanky character
        CombatManager.Instance.Characters.Sort((a, b) => (a.Emnity.CompareTo(b.Emnity)));
        PerformSkill(new List<Unit>() { CombatManager.Instance.Characters[0] });
    }

    public override void PerformSkill(List<Unit> Targets)
    {
        this.Targets = Targets;
        StartCoroutine(Performing());
    }
}
