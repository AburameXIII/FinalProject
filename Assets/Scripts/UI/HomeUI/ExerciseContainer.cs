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



    public void UpdateExercise(ExerciseDetail ed, MeasuringMethod m)
    {
        ExerciseName.text = ed.Exercise.ExerciseName;
        ExerciseAmount.text = ed.GetObjective();

        switch (m)
        {
            case MeasuringMethod.GPS:
                ExercisePace.text = "GPS";
                break;
            case MeasuringMethod.Pace:
                ExercisePace.text = "Constant Pace " + ed.Pace.x + " : " + ed.Pace.y;
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

        Instantiate(ed.Exercise.SmallExercisePreview, ExercisePreview);

    }
}
