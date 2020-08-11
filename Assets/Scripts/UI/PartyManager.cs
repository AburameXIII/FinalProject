using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public static PartyManager Instance { set; get; }

    public List<Character> Party;

    public List<Character> AllCharacters;

    public Dictionary<Character, Progression> CharacterProgression;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        CharacterProgression = new Dictionary<Character, Progression>();

        //try to load save progression from files, else, reset
        foreach (Character c in AllCharacters)
        {
            CharacterProgression.Add(c, new Progression());
        }
    }


    void Update()
    {
        
    }

    public Progression GetProgression(Character c)
    {
        Progression p = null;
        CharacterProgression.TryGetValue(c, out p);
        return p;
    }
}


