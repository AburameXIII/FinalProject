using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WorkoutObjective
{
    Time, Amount, DistanceKilometers, DistanceMeters
}

public enum Equipment
{
    Dumbbell, Trainers
}

public enum MeasuringMethod
{
    None, GPS, Step, Pace, Time
}


[CreateAssetMenu(menuName = "Exercise")]
public class Exercise : ScriptableObject
{
    public string ExerciseName;
    public List<Equipment> RequiredEquipment;
    public GameObject SmallExercisePreview;
    public GameObject ExercisePreview;

    
}


