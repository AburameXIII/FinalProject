using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SecondaryResourceType
{
    MP, RG, PS, COMBO
}

public enum UnitType
{
    Friendly, Enemy, Neutral
}

/*
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
*/



public abstract class Unit : MonoBehaviour
{
    public string UnitName;

    [HideInInspector]
    public int MaxHP;
    [HideInInspector]
    public int CurrentHP;

    [HideInInspector]
    public UnitType UnitType;

    [HideInInspector]
    public int MaxSecondaryResource;
    [HideInInspector]
    public int CurrentSecondaryResource;
    [HideInInspector]
    public SecondaryResourceType SecondaryResource;

    [HideInInspector]
    public Stat Speed;
    [HideInInspector]
    public Stat Defense;
    [HideInInspector]
    public Stat Attack;
    [HideInInspector]
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

    protected List<ActionSelectionEffect> BeforeActionSelectionEffects;
    protected List<ActionSelectionEffect> AfterActionSelectionEffects;
    protected List<PersitantEffect> PersitantEffects;
    protected List<EndOfTurnEffect> EndOfTurnEffects;


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

        PersitantEffects.Add(e);
        BattleHUD.AddEffect(e);
        e.PerformEffect(this);
    }

    public void AddEndOfTurnEffect(EndOfTurnEffect e)
    {
        if (!e.Stackable && BattleHUD.HasEffect(e)) return;

        EndOfTurnEffects.Add(e);
        BattleHUD.AddEffect(e);
    }

    public void AddAfterActionSelectionEffect(ActionSelectionEffect e)
    {
        if (!e.Stackable && BattleHUD.HasEffect(e)) return;

        AfterActionSelectionEffects.Add(e);
        BattleHUD.AddEffect(e);
    }

    IEnumerator EndOfTurnRoutine()
    {

        foreach(EndOfTurnEffect e in EndOfTurnEffects)
        {
            yield return new WaitForSeconds(0.2f);
            e.PerformEffect(this);
        }
        
        for(int i = EndOfTurnEffects.Count - 1; i >= 0; i--)
        {
            if (EndOfTurnEffects[i].DecreaseDuration())
            {
                BattleHUD.RemoveEffect(EndOfTurnEffects[i]);
                EndOfTurnEffects.RemoveAt(i);
                
            }
        }

        for (int i = PersitantEffects.Count - 1; i >= 0; i--)
        {
            if (PersitantEffects[i].DecreaseDuration())
            {
                PersitantEffects[i].UndoEffect(this);
                
                BattleHUD.RemoveEffect(PersitantEffects[i]);
                PersitantEffects.RemoveAt(i);
            }
        }

        for (int i = BeforeActionSelectionEffects.Count - 1; i >= 0; i--)
        {
            if (BeforeActionSelectionEffects[i].DecreaseDuration())
            {
                
                BattleHUD.RemoveEffect(BeforeActionSelectionEffects[i]);
                BeforeActionSelectionEffects.RemoveAt(i);
            }
        }

        for (int i = AfterActionSelectionEffects.Count - 1; i >= 0; i--)
        {
            if (AfterActionSelectionEffects[i].DecreaseDuration())
            {
                
                BattleHUD.RemoveEffect(AfterActionSelectionEffects[i]);
                AfterActionSelectionEffects.RemoveAt(i);
            }
        }

        CombatManager.Instance.CheckDeaths();
    }

    public void EndOfTurn()
    {
        CombatManager.Instance.State = BattleState.EndTurn;
        StartCoroutine(EndOfTurnRoutine());
    }

    protected virtual void Awake()
    {
        BeforeActionSelectionEffects = new List<ActionSelectionEffect>();
        AfterActionSelectionEffects = new List<ActionSelectionEffect>();
        PersitantEffects = new List<PersitantEffect>();
        EndOfTurnEffects = new List<EndOfTurnEffect>();

        BattleHUD = Instantiate(BattleHUDPrefab, this.transform).GetComponent<BattleHUD>();
        BattleHUD.Setup(this);
    }

    public void PerformSkill(Skill s)
    {
        foreach (ActionSelectionEffect e in AfterActionSelectionEffects)
        {
            if (e.PerformEffect(this))
            {
                //DID NOT PERFORM SKILL
                EndOfTurn();
                return;
            }


        }

        StartCoroutine(s.Performing());
    }

}
