using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SecondaryResourceType
{
    MP, RG, PS
}

public enum UnitType
{
    Friendly, Enemy, Neutral
}

[Serializable]
public class Pair<T, U>
{
    public Pair()
    {
    }

    public Pair(T first, U second)
    {
        this.First = first;
        this.Second = second;
    }

    public T First { get; set; }
    public U Second { get; set; }
};




public abstract class Unit : MonoBehaviour
{
    public string UnitName;

    public int MaxHP;
    public int CurrentHP;

    [HideInInspector]
    public UnitType UnitType;

    public int MaxSecondaryResource;
    public int CurrentSecondaryResource;
    public SecondaryResourceType SecondaryResource;

    public Stat Speed;
    public Stat Defense;
    public Stat Attack;
    public Stat Luck;

    //CHANGE WAY OF CALCULATING TURNS
    [HideInInspector]
    public float CurrentTimeToNextTurn;
    [HideInInspector]
    public int Emnity;

    public GameObject BattleHUDPrefab;
    private BattleHUD BattleHUD { get; set; }

    [HideInInspector]
    public Sprite TurnSprite;

    public GameObject DamageTextPrefab;

    protected List<Pair<ActionSelectionEffect,int>> BeforeActionSelectionEffects;
    protected List<Pair<ActionSelectionEffect,int>> AfterActionSelectionEffects;
    protected List<Pair<PersitantEffect,int>> PersitantEffects;
    protected List<Pair<EndOfTurnEffect,int>> EndOfTurnEffects;


    public void TakeDamage(float MinAttackMultiplier, float MaxAttackMultiplier, Unit AttackUnit)
    {
        float AttackMultiplier = UnityEngine.Random.Range(MinAttackMultiplier, MaxAttackMultiplier);
        int Damage = Mathf.RoundToInt( AttackUnit.Attack.Value * AttackMultiplier * (1 - CombatManager.Instance.DefenseResistanceCurve.Evaluate(this.Defense.Value/999f)));



        

        float CriticalChance = CombatManager.Instance.LuckCriticalCurve.Evaluate(AttackUnit.Luck.Value/999f);
        float Chance = UnityEngine.Random.Range(0f, 1f);
        if(Chance < CriticalChance)
        {
            Damage *= 2;
            DamageUI floatingText = Instantiate(DamageTextPrefab, this.transform).GetComponent<DamageUI>();
            floatingText.SetUpCriticalDamage(Damage);
        } else
        {
            DamageUI floatingText = Instantiate(DamageTextPrefab, this.transform).GetComponent<DamageUI>();
            floatingText.SetUpDamage(Damage);
        }

        CurrentHP = Mathf.Clamp(CurrentHP - Damage, 0, MaxHP);
        //PERHAPS ADD IN THE FUTURE STATEMENTS THAT CHECK IF THERE IS AN HUD OR NOT, ALTHOUGH INITIALLY, ALL UNITS SHOULD HAVE ONE
        BattleHUD.ChangeHealth(CurrentHP);
    }


    public void ChangeHealth(int Amount)
    {
        CurrentHP = Mathf.Clamp(CurrentHP + Amount, 0, MaxHP);

        //PERHAPS ADD IN THE FUTURE STATEMENTS THAT CHECK IF THERE IS AN HUD OR NOT, ALTHOUGH INITIALLY, ALL UNITS SHOULD HAVE ONE
        BattleHUD.ChangeHealth(CurrentHP);
    }

    public void ResetCurrentTimeToNextTurn()
    {
        CurrentTimeToNextTurn = 1000 / Speed.Value;
    }




    public void ChangeSecondary(int Amount)
    {
        CurrentSecondaryResource = Mathf.Clamp(CurrentSecondaryResource + Amount, 0, MaxSecondaryResource);

        //PERHAPS ADD IN THE FUTURE STATEMENTS THAT CHECK IF THERE IS AN HUD OR NOT, ALTHOUGH INITIALLY, ALL UNITS SHOULD HAVE ONE
        BattleHUD.ChangeSecondary(CurrentSecondaryResource);
    }

    


    public void AddPersistantEffect(PersitantEffect e)
    {
        if (!e.Stackable && BattleHUD.HasEffect(e)) return;

        PersitantEffects.Add(new Pair<PersitantEffect, int>(e,e.TurnDuration));
        BattleHUD.AddEffect(e);
        e.PerformEffect(this);
    }

    public void AddEndOfTurnEffect(EndOfTurnEffect e)
    {
        if (!e.Stackable && BattleHUD.HasEffect(e)) return;

        EndOfTurnEffects.Add(new Pair<EndOfTurnEffect, int>(e, e.TurnDuration));
        BattleHUD.AddEffect(e);
    }

    public void AddAfterActionSelectionEffect(ActionSelectionEffect e)
    {
        if (!e.Stackable && BattleHUD.HasEffect(e)) return;

        AfterActionSelectionEffects.Add(new Pair<ActionSelectionEffect, int>(e, e.TurnDuration));
        BattleHUD.AddEffect(e);
    }

    IEnumerator EndOfTurnRoutine()
    {

        foreach(Pair<EndOfTurnEffect,int> p in EndOfTurnEffects)
        {
            yield return new WaitForSeconds(0.2f);
            p.First.PerformEffect(this);
        }
        
        EndOfTurnEffects.RemoveAll(delegate (Pair<EndOfTurnEffect, int> p)
        {
            if (--p.Second == 0)
            {
                BattleHUD.RemoveEffect(p.First);
                return true;
            }
            return false;
        });

        PersitantEffects.RemoveAll(delegate (Pair<PersitantEffect, int> p)
        {
            if (--p.Second == 0)
            {
                p.First.UndoEffect(this);
                BattleHUD.RemoveEffect(p.First);
                return true;
            }
            return false;
        });

        BeforeActionSelectionEffects.RemoveAll(delegate (Pair<ActionSelectionEffect, int> p)
        {
            if (--p.Second == 0)
            {
                BattleHUD.RemoveEffect(p.First);
                return true;
            }
            return false;
        });


        AfterActionSelectionEffects.RemoveAll(delegate (Pair<ActionSelectionEffect, int> p)
        {
            if (--p.Second == 0)
            {
                BattleHUD.RemoveEffect(p.First);
                return true;
            }
            return false;
        });

        CombatManager.Instance.CheckDeaths();
    }

    public void EndOfTurn()
    {
        CombatManager.Instance.State = BattleState.EndTurn;
        StartCoroutine(EndOfTurnRoutine());
    }

    protected virtual void Awake()
    {
        BeforeActionSelectionEffects = new List<Pair<ActionSelectionEffect, int>>();
        AfterActionSelectionEffects = new List<Pair<ActionSelectionEffect, int>>();
        PersitantEffects = new List<Pair<PersitantEffect, int>>();
        EndOfTurnEffects = new List<Pair<EndOfTurnEffect, int>>();



        Debug.Log("AWAKE " + UnitName);
        BattleHUD = Instantiate(BattleHUDPrefab, this.transform).GetComponent<BattleHUD>();
        BattleHUD.Setup(this);
    }

    public void PerformSkill(Skill s)
    {
        foreach (Pair<ActionSelectionEffect, int> e in AfterActionSelectionEffects)
        {
            if (e.First.PerformEffect(this))
            {
                //DID NOT PERFORM SKILL
                EndOfTurn();
                return;
            }


        }

        StartCoroutine(s.Performing());
    }

}
