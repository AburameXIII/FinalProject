using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryUI : MonoBehaviour
{
    public GameObject CharacterEXPGainPrefab;
    public Transform SpawnStart;

    void Awake()
    {
        Debug.Log("AWAKENING VICTORY UI");
        foreach (Character c in PartyManager.Instance.Party)
        {
            ProgressionAnimated PA = Instantiate(CharacterEXPGainPrefab, SpawnStart.position, Quaternion.identity, this.transform).GetComponent<ProgressionAnimated>();
            PA.SetProgression(c, PartyManager.Instance.GetFightProgression(c), c.CharacterSecondaryColor);
            SpawnStart.position -= new Vector3(0, 500, 0);
        }

        
    }



}
