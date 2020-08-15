using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CharacterProgress
{
    public Progression FightProgression;
    public Progression TrainProgression;

    public CharacterProgress(Progression F, Progression T)
    {
        FightProgression = F;
        TrainProgression = T;
    }
}

public class PartyManager : MonoBehaviour
{
    public static PartyManager Instance { set; get; }

    public List<Character> Party;

    public List<Character> AllCharacters;

    public Dictionary<Character, CharacterProgress> CharacterProgression;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        CharacterProgression = new Dictionary<Character, CharacterProgress>();

        //try to load save progression from files, else, reset
        foreach (Character c in AllCharacters)
        {
            CharacterProgression.Add(c, new CharacterProgress(new Progression(), new Progression()));
        }
    }


    void Update()
    {
        
    }

    public Progression GetFightProgression(Character c)
    {
        CharacterProgress p;
        CharacterProgression.TryGetValue(c, out p);
        return p.FightProgression;
    }

    public Progression GetTrainProgression(Character c)
    {
        CharacterProgress p;
        CharacterProgression.TryGetValue(c, out p);
        return p.TrainProgression;
    }
}


