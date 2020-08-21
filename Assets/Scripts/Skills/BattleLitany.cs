using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class BattleLitany : Skill
{
    private float LuckModifierPercentage;

    public BattleLitany(Unit User, Sprite Sprite) : base(User)
    {
        LuckModifierPercentage = 0.1f;
        SkillName = "Battle Litany";
        SkillDescription = "Increases allies luck by 10% for each Combo Point for 4 turns";
        SkillImage = Sprite;
    }


    public override bool CanPerform()
    {
        //CHECK LEVEL;
        return User.CurrentSecondaryResource > 0;
    }

    

    public override void Perform()
    {
        foreach (Unit u in CombatManager.Instance.Characters)
        {
            //u.TakeDamage(100);
            u.AddPersistantEffect(new IncreaseLuck(LuckModifierPercentage * User.CurrentSecondaryResource, 4));
        }
    }


    public override IEnumerator Performing()
    {
        //DO ANIMATIONS
       
        yield return new WaitForSeconds(1.0f);

        //IN THE ANIMATION CALL Perform() as alternative
        Perform();

        //Go to end of turn actions
        User.EndOfTurn();
    }
}
