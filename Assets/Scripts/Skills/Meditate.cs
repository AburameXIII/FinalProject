using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Meditate : Skill
{
    public List<SkillEffect> Effects;

    public Meditate(Unit User, Sprite Sprite) : base(User)
    {
        SkillName = "Meditate";
        SkillDescription = "Consumes RG to recover HP";
        SkillImage = Sprite;
    }

    public override bool CanPerform()
    {
        //CHECK LEVEL;
        return User.CurrentSecondaryResource >= 1;
    }

    

    public override void Perform()
    {
        User.ChangeHealth(User.CurrentSecondaryResource);
        User.ChangeSecondary(-User.CurrentSecondaryResource);
    }


    public override IEnumerator Performing()
    {
        //DO ANIMATIONS
       
        Debug.Log("performing Meditation");
        yield return new WaitForSeconds(1.0f);

        //IN THE ANIMATION CALL Perform() as alternative
        Perform();

        //Go to end of turn actions
        User.EndOfTurn();
    }
}
