using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class SuperPunch : Skill
{

    public float MinAttackMultiplier;
    public float MaxAttackMultiplier;

    public override bool CanPerform()
    {
        //CHECK LEVEL;
         return u.CurrentSecondaryResource >= 100;
    }

    public override void PerformSkill(List<Unit> Targets)
    {
        this.Targets = Targets;
        StartCoroutine(Performing());
    }

    public override void Perform()
    {
        foreach (Unit t in Targets)
        {
            t.TakeDamage(MinAttackMultiplier,MaxAttackMultiplier, u);
        }
        u.ChangeSecondary(-100);
    }


    public override IEnumerator Performing()
    {
        //DO ANIMATIONS
       
        Debug.Log("performing super punch");
        yield return new WaitForSeconds(1.0f);

        //IN THE ANIMATION CALL Perform() as alternative
        Perform();

        //Go to end of turn actions
        u.EndOfTurn();
    }
}
