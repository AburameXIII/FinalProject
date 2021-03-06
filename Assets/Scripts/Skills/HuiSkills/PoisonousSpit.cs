﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonousSpit : EnemySkill
{
    public override void Perform()
    {
        foreach (Unit t in Targets)
        {
            t.AddEndOfTurnEffect(new Poison());
        }
    }

    public PoisonousSpit(Unit User) : base(User)
    {
        SkillName = "Poisonous Spit";
    }

    public override IEnumerator Performing()
    {
        Targets = CombatManager.Instance.Characters;

        yield return new WaitForSeconds(0.5f);

        Perform();

        //Go to end of turn actions
        User.EndOfTurn();
    }


}
