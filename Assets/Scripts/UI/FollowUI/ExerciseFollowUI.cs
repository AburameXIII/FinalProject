using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseFollowUI : MonoBehaviour
{
    
    public Transform ExercisePosition;
    public Button NextExercise;
    public Button PreviousExercise;
    public Button Done;

    public Text ExerciseNameText;

    public Exercise e;
    private Color Color;

    public Bar HorizontalBar;
    public DistanceTracker DistanceTracker;

    public Text Amount;
    public Button ExitButton;

    private MeasuringMethod m;
    private IWorkoutObjective ObjectiveMeasure;

    private GameObject ExercisePreview;

    // Start is called before the first frame update
    void Start()
    {
        Color = UIManager.CurrentWorkoutCharacter.CharacterColor;
        HorizontalBar.SetColor(Color);
        ExitButton.onClick.AddListener(delegate { UIManager.Instance.FinishWorkout(); });
    }



    public void SetUpExercise(int ExerciseNumber)
    {
        HorizontalBar.Countdown = false;
        HorizontalBar.gameObject.SetActive(false);
        Amount.gameObject.SetActive(false);

        e =  UIManager.CurrentWorkout.Exercises[ExerciseNumber];
        ExerciseNameText.text = e.ExerciseName;


        if (ExercisePreview != null) Destroy(ExercisePreview);
        ExercisePreview = Instantiate(e.ExercisePreview, ExercisePosition);


        if(ExerciseNumber == 0)
        {
            PreviousExercise.gameObject.SetActive(false);
        }  else
        {
            PreviousExercise.gameObject.SetActive(true);
        }
        
        if (ExerciseNumber == UIManager.CurrentWorkout.Exercises.Count - 1)
        {
            NextExercise.gameObject.SetActive(false);
        } else
        {
            NextExercise.gameObject.SetActive(true);
        }


        m = UIManager.ChosenMeasuringMethods[ExerciseNumber];
        switch (m)
        {
            case MeasuringMethod.GPS:

                switch (e.ObjectiveType)
                {
                    case WorkoutObjective.DistanceKilometers:
                        HorizontalBar.SetTextForm("{0:0.0} / {1:0.0} Km");
                        break;
                    case WorkoutObjective.DistanceMeters:
                        HorizontalBar.SetTextForm("{0:0} / {1:0} m");
                        break;
                }

                HorizontalBar.ChangeCurrentMax(0, e.ObjectiveQuantity);
                HorizontalBar.gameObject.SetActive(true);

                ObjectiveMeasure = DistanceTracker;
                ObjectiveMeasure.Setup(e.ObjectiveQuantity);

                break;
            case MeasuringMethod.Pace:
                

                switch (e.ObjectiveType)
                {
                    case WorkoutObjective.Amount:
                        HorizontalBar.SetTextForm("{0} / {1}");
                        break;
                    case WorkoutObjective.DistanceKilometers:
                        HorizontalBar.SetTextForm("{ 0:0.0} / { 1:0.0} Km");
                        break;
                    case WorkoutObjective.DistanceMeters:
                        HorizontalBar.SetTextForm("{0:0} / {1:0} m");
                        break;
                }
                HorizontalBar.gameObject.SetActive(true);

                HorizontalBar.ChangeCurrentMax(0, e.ObjectiveQuantity);
                break;
            case MeasuringMethod.Time:

                HorizontalBar.gameObject.SetActive(true);

                ObjectiveMeasure = HorizontalBar;
                ObjectiveMeasure.Setup(e.ObjectiveQuantity);

                break;
            case MeasuringMethod.None:
                switch (e.ObjectiveType)
                {
                    case (WorkoutObjective.Amount):
                        Amount.text = "x " + e.ObjectiveQuantity;
                        break;
                    case WorkoutObjective.DistanceKilometers:
                        Amount.text = string.Format("{0:0.0} Km", e.ObjectiveQuantity);
                        break;
                    case WorkoutObjective.DistanceMeters:
                        Amount.text = string.Format("{0:0} m", e.ObjectiveQuantity);
                        break;
                    case (WorkoutObjective.Time):
                        int TotalSeconds = e.ObjectiveQuantity;
                        int Minutes = TotalSeconds / 60;
                        int Seconds = TotalSeconds % 60;
                        Amount.text = string.Format("{0:D2}:{1:D2}", Minutes, Seconds);
                        break;
                }
                Amount.gameObject.SetActive(true);

                break;
        }

    }

    public void StartExercise()
    {
        if(ObjectiveMeasure!= null)
        {
            ObjectiveMeasure.StartMeasuring();
        }
    }


    void Update()
    {
        if (ObjectiveMeasure != null && ObjectiveMeasure.IsCompleted())
        {
            WorkoutFollowUI.Instance.SwitchToRestUI();
            ObjectiveMeasure = null;
        }
    }

    public void FinishExercise()
    {
        WorkoutFollowUI.Instance.SwitchToRestUI();
        ObjectiveMeasure = null;
    }

    public void GoBack()
    {
        WorkoutFollowUI.Instance.GoBack();
        ObjectiveMeasure = null;
    }
}
