using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : MonoBehaviour
{
    public List<CharacterSlot> CharacterSlots;
    public GameObject CharacterInfoPrefab;

    void Start()
    {
        ShowParty();
    }

    public void ShowParty()
    {
        foreach(CharacterSlot cs in CharacterSlots)
        {
            cs.Setup();
        }
        for(int i = 0; i < PartyManager.Instance.Party.Count; i++)
        {
            CharacterInfoUI Info = Instantiate(CharacterInfoPrefab, CharacterSlots[i].transform).GetComponent<CharacterInfoUI>();
            Info.Setup(PartyManager.Instance.Party[i]);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
