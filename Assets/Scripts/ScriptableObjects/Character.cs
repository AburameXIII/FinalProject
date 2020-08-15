using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Character")]
public class Character : ScriptableObject
{
    public string CharacterName;
    public Sprite CharacterProfilePicture;
    public List<Workout> Workouts;
    public Color CharacterPrimaryColor;
    public Color CharacterSecondaryColor;
    public GameObject CharacterPrefab;
}
