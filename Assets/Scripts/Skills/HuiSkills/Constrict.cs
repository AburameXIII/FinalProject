using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constrict : EnemySkill
{

    public override void Perform()
    {
        foreach (Unit t in Targets)
        {
            t.AddAfterActionSelectionEffect(new Paralyze(3));
        }
    }

    public Constrict(Unit User): base(User)
    {
        SkillName = "Constrict";
    }

    public override IEnumerator Performing()
    {
        Targets = new List<Unit>() { CombatManager.Instance.Characters[Random.Range(0, CombatManager.Instance.Characters.Count)] };

        yield return new WaitForSeconds(0.5f);

        Perform();

        //Go to end of turn actions
        User.EndOfTurn();
    }


}
