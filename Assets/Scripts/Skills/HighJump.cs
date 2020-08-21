using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class HighJump : Skill
{
    public float MinAttackMultiplier;
    public float MaxAttackMultiplier;

    public HighJump(Unit User, Sprite Sprite): base(User)
    {
        MinAttackMultiplier = 1.0f;
        MaxAttackMultiplier = 1.2f;
        SkillName = "High Jump";
        SkillDescription = "Jumps higher and deals a good amount of damage to a single target";
        SkillImage = Sprite;
    }

    public override bool CanPerform()
    {
        //CHECK LEVEL;
        return User.CurrentSecondaryResource >= 1;
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
