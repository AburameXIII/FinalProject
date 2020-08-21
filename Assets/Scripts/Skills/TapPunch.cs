using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class TapPunch : Skill
{
    public float MinAttackMultiplier;
    public float MaxAttackMultiplier;

    public TapPunch(Unit User, Sprite Sprite): base(User)
    {
        MinAttackMultiplier = 0.4f;
        MaxAttackMultiplier = 0.5f;
        SkillName = "Tap Punch";
        SkillDescription = "Punches a single enemy and regenerates 10 RG";
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
        User.ChangeSecondary(10);
    }


    public override IEnumerator Performing()
    {
        Targets = CombatManager.Instance.Enemies;
        //DO ANIMATIONS
       
        Debug.Log("performing tap punch");
        yield return new WaitForSeconds(1.0f);

        //IN THE ANIMATION CALL Perform() as alternative
        Perform();

        //Go to end of turn actions
        User.EndOfTurn();
    }
}
