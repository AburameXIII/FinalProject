using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { Start, ActionChoose, ActionPerform, EndTurn, Win, Lost }
public enum FightType { Standard, Boss, Horde }

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance { get; set; }

    public List<Unit> Characters;

    public List<Unit> Enemies;

    public List<Unit> AliveUnits;
    public List<Unit> DeadUnits;

    public List<Unit> Turns;
    public Turns TurnsUI;

    public BattleState State;

    public List<Transform> SpawnPositions;
    public List<Transform> EnemySpawnPositions;

    public Actions Actions;
    private Level Level;

    public AnimationCurve DefenseResistanceCurve;
    public AnimationCurve LuckCriticalCurve;

    private Unit UnitChoosing;

    public EndUI DefeatUI;
    public EndUI VictoryUI;

    [Header("Effect Icons")]
    public Sprite PoisonIcon;
    public Sprite ParalyzeIcon;
    public Sprite AttackUpIcon;

    public Sprite  GetEffectIcon(Effect e)
    {
        
        switch (e)
        {
            case Effect.AttackUp:
                return AttackUpIcon;
            case Effect.Paralyze:
                return ParalyzeIcon;
            case Effect.Poison:
                return PoisonIcon;
            default:
                return null;
        }
    }

    public void Retry()
    {
        if(State == BattleState.Lost)
        {
            DefeatUI.FadeOut();
            State = BattleState.Start;
            StartCombat();
        }
    }

    public void StartCombat()
    {
        if (State == BattleState.Start)
        {
            ResetValues();

            SpawnCharacters(PartyManager.Instance.Party);
            SpawnEnemies(Level.Enemies, Level.FightType);


            //Calculate turns
            CalculateTurns(8);
            TurnsUI.Setup(Turns);



            //EVENTUALLY A TEXT INTRO MAYBE?
            StartCoroutine(CombatStartDialogue());
        }
    }
    

    public void StartCombat(Level l)
    {
        Level = l;
        StartCombat();
    }


    void ResetValues()
    {
        foreach (Unit u in AliveUnits)
        {
            Debug.Log("DESTROYING");
            Destroy(u.gameObject);
        }

        foreach (Unit u in DeadUnits)
        {
            Debug.Log("DESTROYING");
            Destroy(u.gameObject);
        }

        Characters = new List<Unit>();
        Enemies = new List<Unit>();
        AliveUnits = new List<Unit>();
        Turns = new List<Unit>();
        DeadUnits = new List<Unit>();
    }

    void SpawnCharacters(List<Character> CharacterInfo)
    {
        if (CharacterInfo.Count == 2)
        {
            Unit CharacterSpawned = Instantiate(CharacterInfo[0].CharacterPrefab, SpawnPositions[3]).GetComponent<Unit>();
            Characters.Add(CharacterSpawned);
            AliveUnits.Add(CharacterSpawned);

            Unit CharacterSpawned2 = Instantiate(CharacterInfo[1].CharacterPrefab, SpawnPositions[4]).GetComponent<Unit>();
            Characters.Add(CharacterSpawned2);
            AliveUnits.Add(CharacterSpawned2);

        }
        else
        {
            for(int i = 0; i < CharacterInfo.Count; i++)
            {
                Unit CharacterSpawned = Instantiate(CharacterInfo[i].CharacterPrefab, SpawnPositions[i]).GetComponent<Unit>();
                Characters.Add(CharacterSpawned);
                AliveUnits.Add(CharacterSpawned);
            }
        }
    }

    void SpawnEnemies(List<Enemy> EnemyInfo, FightType FightType)
    {
        switch (FightType)
        {
            case FightType.Boss:
                Unit EnemySpawned = Instantiate(EnemyInfo[0].EnemyPrefab, EnemySpawnPositions[0]).GetComponent<Unit>();
                Enemies.Add(EnemySpawned);
                AliveUnits.Add(EnemySpawned);
                break;

            case FightType.Standard:
                break;

            case FightType.Horde:
                break;

        }
    }


    IEnumerator CombatStartDialogue()
    {

        yield return new WaitForSeconds(1.0f);

        
        ChooseAction();
    }

    public void PerformingAction()
    {
        State = BattleState.ActionPerform;
    }

    void ChooseAction()
    {
        State = BattleState.ActionChoose;
        UnitChoosing = Turns[0];
        switch (UnitChoosing.UnitType)
        {
            case UnitType.Enemy:
                (UnitChoosing as EnemyUnit).PerformAction();
                //Perform Action With COUROUTINE AND AT THE END PASS TURN

                break;

            case UnitType.Friendly:
                //UPDATE BUTTONS UI
                Actions.Appear(Turns[0] as CharacterUnit);
                break;
        }

    }


    public void CalculateTurns(int amount)
    {
        int count = 0;
        while (count < amount)
        {
            float min = Mathf.Infinity;
            foreach (Unit u in AliveUnits)
            {
                if (u.CurrentTimeToNextTurn < min)
                {
                    min = u.CurrentTimeToNextTurn;
                }
            }

            List<Unit> NextTurns = new List<Unit>();
            foreach (Unit u in AliveUnits)
            {
                u.CurrentTimeToNextTurn -= min;
                if (u.CurrentTimeToNextTurn == 0)
                {
                    NextTurns.Add(u);
                }
            }

            NextTurns.OrderBy(o => o.Speed.Value);
            Unit UnitToAdd = NextTurns[0];
            UnitToAdd.ResetCurrentTimeToNextTurn();
            count++;
            Turns.Add(UnitToAdd);
        }
    }

    


    void Awake()
    {
        Instance = this;
        State = BattleState.Start;

        //SET UP LEVEL
    }

    private void Start()
    {
        StartCombat(UIManager.CurrentLevel);

        UIManager.Instance.FadeIn();

    }

    public void NextTurn()
    {
        Turns.RemoveAt(0);
        CalculateTurns(1);
        TurnsUI.NextTurn(Turns[Turns.Count - 1]);

        ChooseAction();
    }


    // Update is called once per frame
    void Update()
    {
        
    }



    public void CheckDeaths()
    {
        StartCoroutine(CheckDeathsRoutine());
    }

   

    IEnumerator CheckDeathsRoutine()
    {
        //REMOVE DEAD CHARACTER TURNS
        List<int> TurnsToRemove = new List<int>();

        foreach (Unit u in AliveUnits)
        {
            if(u.CurrentHP <= 0)
            {
                

                Characters.Remove(u);
                Enemies.Remove(u);
                DeadUnits.Add(u);

                
                //SAVE WHICH TURNS TO REMOVE
                for (int i = 0; i < Turns.Count; i++)
                {
                    if(Turns[i] == u)
                    {
                        TurnsToRemove.Add(i);
                        

                    }
                }
                

                //Perform death animation
                yield return new WaitForSeconds(0.2f);
            }
        }


        //REMOVE DEAD CHARACTERS FROM UNITS
        TurnsToRemove.Sort();
        AliveUnits.RemoveAll(delegate (Unit u)
        {
            return u.CurrentHP <= 0;
        });

        //REMOVE TURNS
        int count = 0;
        foreach (int i in TurnsToRemove)
        {
            Debug.Log("removed turn " + (i - count));
            Turns.RemoveAt(i - count);
            CalculateTurns(1);
            TurnsUI.Discard(i - count, Turns[Turns.Count - 1]);
            count++;
        }




        yield return new WaitForSeconds(0.5f);
        if (Characters.Count == 0)
        {
            DefeatUI.gameObject.SetActive(true);
            DefeatUI.FadeIn();
            State = BattleState.Lost;
        } else if (Enemies.Count == 0)
        {
            VictoryUI.gameObject.SetActive(true);
            VictoryUI.FadeIn();
            State = BattleState.Win;
            Debug.Log("YOU WIN");
            StartCoroutine(VictoryEXP());
        } else
        {
            NextTurn();

        }


        
    }


    IEnumerator VictoryEXP()
    {
        yield return new WaitForSeconds(VictoryUI.LerpDuration);
        foreach (Character c in PartyManager.Instance.Party)
        {
            PartyManager.Instance.GetFightProgression(c).AddExperience(Level.Experience);
        }
    }
}
