using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Warcry : Skill
{
    public IncreaseAttack IncreaseAttack;

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
        foreach (Unit u in CombatManager.Instance.Characters)
        {
            //u.TakeDamage(100);
            u.AddPersistantEffect(IncreaseAttack);
        }
        u.ChangeSecondary(40);
    }


    public override IEnumerator Performing()
    {
        //DO ANIMATIONS
       
        Debug.Log("performing Warcry");
        yield return new WaitForSeconds(1.0f);

        //IN THE ANIMATION CALL Perform() as alternative
        Perform();

        //Go to end of turn actions
        u.EndOfTurn();
    }
}
