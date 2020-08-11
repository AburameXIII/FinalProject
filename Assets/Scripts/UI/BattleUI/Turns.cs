using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Turns : MonoBehaviour
{
    public List<Turn> t;
    public List<Transform> Locations;


    public GameObject TurnPrefab;

    public void Setup(List<Unit> turns)
    {
        t = new List<Turn>();
        for(int i = 0; i < 8; i++)
        {
            Turn NewTurn = Instantiate(TurnPrefab, Locations[i].position, Quaternion.identity, this.transform).GetComponent<Turn>();
            NewTurn.SetSprite(turns[i]);
            t.Add(NewTurn);
        }
        t[0].GoFirst(Locations[0]);
    }

    public void NextTurn(Unit u)
    {
        //DELETE FIRST
        t[0].GoUp();

        //MAKE SECOND FIRST
        t[1].GoFirst(Locations[0]);

        //SHIFT THE REST        
        for(int i = 2; i < 8; i++)
        {
            t[i].GoNext(Locations[i - 1]);
        }

        for(int i = 0; i < 7; i++)
        {
            t[i] = t[i + 1];
        }

        //Instantiates new turn
        t[7] = Instantiate(TurnPrefab, Locations[7].position, Quaternion.identity, this.transform).GetComponent<Turn>();
        t[7].SetSprite(u);

    }

    public void Discard(int i, Unit u)
    {
        if (i == 0)
        {
            NextTurn(u);
            return;
        }

        t[i].GoUp();

        for(int j = i; j < 8; j++)
        {
            t[j].GoNext(Locations[j - 1]);
        }

        for (int j = i; j < 7; j++)
        {
            t[j] = t[j + 1];
        }

        //Instantiates new turn
        t[7] = Instantiate(TurnPrefab, Locations[7].position, Quaternion.identity, this.transform).GetComponent<Turn>();
        t[7].SetSprite(u);

    }

    


    void Update()
    {
        
    }
}
