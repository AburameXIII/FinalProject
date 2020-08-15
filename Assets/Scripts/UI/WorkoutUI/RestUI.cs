using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestUI : MonoBehaviour
{
    
    public Text NextExerciseNumber;
    public Text NextExerciseName;
    public Text NextExerciseAmount;
    public Transform ExercisePreviewLocation;
    private GameObject ExercisePreview;

    private Color Color;

    public Bar HorizontalBar;
    public Button ExitButton;

    bool OnGoing = false;

    void Start()
    {
        Color = UIManager.CurrentWorkoutCharacter.CharacterSecondaryColor;
        HorizontalBar.SetColor(Color);
        ExitButton.onClick.AddListener(delegate { UIManager.Instance.FinishWorkout(); });
    }


    public void SetupRest(int ExerciseNumber)
    {
        Exercise NextExercise = UIManager.CurrentWorkout.Exercises[ExerciseNumber+1];

        NextExerciseNumber.text = "Next " + (ExerciseNumber + 2) + " / " + UIManager.CurrentWorkout.Exercises.Count;
        NextExerciseName.text = NextExercise.ExerciseName;
        NextExerciseAmount.text = NextExercise.GetObjective();

        HorizontalBar.Setup(UIManager.CurrentWorkout.Exercises[ExerciseNumber].RestTime);

        if (ExercisePreview != null) Destroy(ExercisePreview);
        ExercisePreview = Instantiate(NextExercise.ExercisePreview, ExercisePreviewLocation);
    }

    public void StartRest()
    {
        HorizontalBar.StartMeasuring();
        OnGoing = true;
    }

    public void SkipRest()
    {
        HorizontalBar.Stop();
        WorkoutFollowUI.Instance.SwitchToExerciseUI();
    }

    void Update()
    {
        if (HorizontalBar.IsCompleted() && OnGoing)
        {
            WorkoutFollowUI.Instance.SwitchToExerciseUI();
            OnGoing = false;
        }
    }
}
