using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Workout")]
public class Workout : ScriptableObject
{
    public string WorkoutName;
    public int Difficulty;
    public int EstimatedDuration;
    public List<Exercise> Exercises;
}

