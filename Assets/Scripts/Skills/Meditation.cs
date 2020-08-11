using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Meditation : Skill
{
    public List<SkillEffect> Effects;

    public override bool CanPerform()
    {
        //CHECK LEVEL;
        return u.CurrentSecondaryResource >= 1;
    }

    public override void PerformSkill(List<Unit> Targets)
    {
        this.Targets = Targets;
        StartCoroutine(Performing());
    }

    public override void Perform()
    {
        u.ChangeHealth(u.CurrentSecondaryResource);
        u.ChangeSecondary(-u.CurrentSecondaryResource);
    }


    public override IEnumerator Performing()
    {
        //DO ANIMATIONS
       
        Debug.Log("performing Meditation");
        yield return new WaitForSeconds(1.0f);

        //IN THE ANIMATION CALL Perform() as alternative
        Perform();

        //Go to end of turn actions
        u.EndOfTurn();
    }
}
