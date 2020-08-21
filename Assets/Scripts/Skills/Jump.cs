using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Jump : Skill
{
    public float MinAttackMultiplier;
    public float MaxAttackMultiplier;

    public Jump(Unit User, Sprite Sprite): base(User)
    {
        MinAttackMultiplier = 0.4f;
        MaxAttackMultiplier = 0.5f;
        SkillName = "Jump";
        SkillDescription = "Jumps and attacks a single target";
        SkillImage = Sprite;
    }

    public override bool CanPerform()
    {
        //CHECK LEVEL;
        return true;
    }

   

    public override void Perform()
    {
        foreach (Unit t in Targets)
        {
            t.TakeDamage(MinAttackMultiplier, MaxAttackMultiplier, User);
        }
        User.ChangeSecondary(1);
    }


    public override IEnumerator Performing()
    {
        Targets = CombatManager.Instance.Enemies;
        //DO ANIMATIONS
       
        yield return new WaitForSeconds(1.0f);

        //IN THE ANIMATION CALL Perform() as alternative
        Perform();

        //Go to end of turn actions
        User.EndOfTurn();
    }
}
