using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Character")]
public class Character : ScriptableObject
{
    public string CharacterName;
    public Sprite CharacterProfilePicture;
    public List<WorkoutUnlock> WorkoutUnlocks;
    public Color CharacterPrimaryColor;
    public Color CharacterSecondaryColor;
    public GameObject CharacterPrefab;

    public int[] HP;
    public int[] Attack;
    public int[] Defense;
    public int[] Speed;
    public int[] Luck;
}


[System.Serializable]
public struct WorkoutUnlock
{
    [SerializeField] public Workout Workout;
    [SerializeField] public int Level;
}