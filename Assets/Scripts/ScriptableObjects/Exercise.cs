using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WorkoutObjective
{
    Time, Amount, DistanceKilometers, DistanceMeters
}

public enum WorkoutExercise
{
    Run, Walk, BicepCurls, Squats
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
    public WorkoutObjective ObjectiveType;
    public List<MeasuringMethod> PossibleMeasuringMethod;
    public WorkoutExercise ExerciseType;
    public int ObjectiveQuantity;
    public Vector2 Pace;
    public int RestTime;
    public List<Equipment> RequiredEquipment;
    public GameObject SmallExercisePreview;
    public GameObject ExercisePreview;

    public string GetObjective()
    {
        switch (ObjectiveType)
        {
            case WorkoutObjective.Amount:
                return "x " + ObjectiveQuantity;
            case WorkoutObjective.Time:
                int TotalSeconds = ObjectiveQuantity;
                int Minutes = TotalSeconds / 60;
                int Seconds = TotalSeconds % 60;
                return string.Format("{0:D2}:{1:D2}", Minutes, Seconds);
            default:
                return "";
        }
    }
}


