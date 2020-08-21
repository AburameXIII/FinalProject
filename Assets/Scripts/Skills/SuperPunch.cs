using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class SuperPunch : Skill
{

    private float MinAttackMultiplier;
    private float MaxAttackMultiplier;

    public SuperPunch(Unit User, Sprite Sprite) : base(User)
    {
        MinAttackMultiplier = 1.2f;
        MaxAttackMultiplier = 1.3f;
        SkillName = "Super Punch";
        SkillDescription = "Consumes 100 RG to deal massive damage to a single unit";
        SkillImage = Sprite;
    }

    public override bool CanPerform()
    {
        //CHECK LEVEL;
         return User.CurrentSecondaryResource >= 100;
    }


    public override void Perform()
    {
        foreach (Unit t in Targets)
        {
            t.TakeDamage(MinAttackMultiplier,MaxAttackMultiplier, User);
        }
        User.ChangeSecondary(-100);
    }


    public override IEnumerator Performing()
    {
        //REPLACE BY A TARGET SYSTEM
        Targets = CombatManager.Instance.Enemies;

        //DO ANIMATIONS
       
        Debug.Log("performing super punch");
        yield return new WaitForSeconds(1.0f);

        //IN THE ANIMATION CALL Perform() as alternative
        Perform();

        //Go to end of turn actions
        User.EndOfTurn();
    }
}
