using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkoutCompletedUI : MonoBehaviour
{

    public GameObject CharacterEXPGainPrefab;
    public Transform SpawnStart;
    public Button ContinueButton;

    void Awake()
    {
        foreach (Character c in PartyManager.Instance.Party)
        {
            ProgressionAnimated PA = Instantiate(CharacterEXPGainPrefab, SpawnStart.position, Quaternion.identity, this.transform).GetComponent<ProgressionAnimated>();
            PA.SetProgression(c, PartyManager.Instance.GetTrainProgression(c), c.CharacterPrimaryColor);
            SpawnStart.position -= new Vector3(0, 500, 0);
        }

        ContinueButton.onClick.AddListener(delegate { UIManager.Instance.FinishWorkout(); });
    }
}
