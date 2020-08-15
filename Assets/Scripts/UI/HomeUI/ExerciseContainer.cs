using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ExerciseContainer : MonoBehaviour
{
    public Transform ExercisePreview;
    public Text ExerciseName;
    public Text ExerciseAmount;
    public Text ExercisePace;



    public void UpdateExercise(Exercise e, MeasuringMethod m)
    {
        ExerciseName.text = e.ExerciseName;
        ExerciseAmount.text = e.GetObjective();

        switch (m)
        {
            case MeasuringMethod.GPS:
                ExercisePace.text = "GPS";
                break;
            case MeasuringMethod.Pace:
                ExercisePace.text = "Constant Pace " + e.Pace.x + " : " + e.Pace.y;
                break;
            case MeasuringMethod.Step:
                ExercisePace.text = "Step Counter";
                break;
            case MeasuringMethod.Time:
                ExercisePace.text = "Timed";
                break;
            case MeasuringMethod.None:
                ExercisePace.text = "";
                break;
        }

        Instantiate(e.SmallExercisePreview, ExercisePreview);

    }
}
