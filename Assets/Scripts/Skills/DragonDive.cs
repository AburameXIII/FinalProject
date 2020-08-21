using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class DragonDive : Skill
{
    public float MinAttackMultiplier;
    public float MaxAttackMultiplier;

    public DragonDive(Unit User, Sprite Sprite): base(User)
    {
        MinAttackMultiplier = 1.8f;
        MaxAttackMultiplier = 2.0f;
        SkillName = "Dragon Dive";
        SkillDescription = "Fully dives into all enemies dealing high damage proportional to the number of combos accumulated.";
        SkillImage = Sprite;
    }

    public override bool CanPerform()
    {
        //CHECK LEVEL;
        return User.CurrentSecondaryResource > 1;
    }

   

    public override void Perform()
    {
        foreach (Unit t in Targets)
        {
            t.TakeDamage(MinAttackMultiplier * User.CurrentSecondaryResource, MaxAttackMultiplier * User.CurrentSecondaryResource, User);
        }
        User.ChangeSecondary(-User.CurrentSecondaryResource);
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
