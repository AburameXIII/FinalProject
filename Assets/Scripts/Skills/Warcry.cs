using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Warcry : Skill
{
    private float AttackModifierPercentage;

    public Warcry(Unit User, Sprite Sprite) : base(User)
    {
        AttackModifierPercentage = 0.1f;
        SkillName = "Warcry";
        SkillDescription = "Increases allies attack by 10% for 4 turns and generates 40 RG";
        SkillImage = Sprite;
    }


    public override bool CanPerform()
    {
        //CHECK LEVEL;
         return true;
    }

    

    public override void Perform()
    {
        foreach (Unit u in CombatManager.Instance.Characters)
        {
            //u.TakeDamage(100);
            u.AddPersistantEffect(new IncreaseAttack(AttackModifierPercentage, 4));
        }
        User.ChangeSecondary(40);
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
