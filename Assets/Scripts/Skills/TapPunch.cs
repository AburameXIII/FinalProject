using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class TapPunch : Skill
{
    public float MinAttackMultiplier;
    public float MaxAttackMultiplier;

    public override bool CanPerform()
    {
        //CHECK LEVEL;
        return true;
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
            t.TakeDamage(MinAttackMultiplier, MaxAttackMultiplier, u);
        }
        u.ChangeSecondary(10);
    }


    public override IEnumerator Performing()
    {
        //DO ANIMATIONS
       
        Debug.Log("performing tap punch");
        yield return new WaitForSeconds(1.0f);

        //IN THE ANIMATION CALL Perform() as alternative
        Perform();

        //Go to end of turn actions
        u.EndOfTurn();
    }
}
