using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenomousBite : EnemySkill
{
    private float MinAttackMultiplier;
    private float MaxAttackMultiplier;

    public VenomousBite(Unit User): base(User)
    {
        MinAttackMultiplier = 1.4f;
        MaxAttackMultiplier = 1.5f;
        SkillName = "Venomous Bite";
    }

    public override void Perform()
    {
        foreach (Unit t in Targets)
        {
            t.TakeDamage(MinAttackMultiplier, MaxAttackMultiplier, User);
            t.AddEndOfTurnEffect(new Poison());
        }
    }

    public override IEnumerator Performing()
    {
        CombatManager.Instance.Characters.Sort((a, b) => (a.Emnity.CompareTo(b.Emnity)));
        Targets = new List<Unit>() { CombatManager.Instance.Characters[0] };
        yield return new WaitForSeconds(0.5f);

       Perform();

        //Go to end of turn actions
        User.EndOfTurn();
    }

}
