using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Workout")]
public class Workout : ScriptableObject
{
    public string WorkoutName;
    public int Difficulty;
    public int EstimatedDuration;
    public List<ExerciseDetail> Exercises;
    public int Experience;
}

[System.Serializable]
public struct ExerciseDetail
{
    [SerializeField] public Exercise Exercise;
    [SerializeField] public WorkoutObjective ObjectiveType;
    [SerializeField] public int ObjectiveQuantity;
    [SerializeField] public Vector2 Pace;
    [SerializeField] public int RestTime;

    public List<MeasuringMethod> GetPossibleMeasuringMethods()
    {
        switch (ObjectiveType)
        {
            case WorkoutObjective.Amount:
                return new List<MeasuringMethod> { MeasuringMethod.None, MeasuringMethod.Step, MeasuringMethod.Pace };
            case WorkoutObjective.DistanceKilometers:
            case WorkoutObjective.DistanceMeters:
                return new List<MeasuringMethod> { MeasuringMethod.None, MeasuringMethod.Step, MeasuringMethod.Pace, MeasuringMethod.GPS };
            case WorkoutObjective.Time:
                return new List<MeasuringMethod> { MeasuringMethod.None, MeasuringMethod.Time };
            default:
                return new List<MeasuringMethod> { MeasuringMethod.None };

        }
    }

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

