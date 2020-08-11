using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseAttack : PersitantEffect
{
    public float Percentage;
    int Amount;

    public override void PerformEffect(Unit u)
    {
        Amount = Mathf.RoundToInt(u.BaseAttack * Percentage);
        u.CurrentAttack = Mathf.Clamp(u.CurrentAttack + Amount, 0, 999);
    }

    public override void UndoEffect(Unit u)
    {
        u.CurrentAttack = Mathf.Clamp(u.CurrentAttack - Amount, 0, 999);
    }

    void Awake()
    {
        Effect = Effect.AttackUp;
        Stackable = true;
    }
}
