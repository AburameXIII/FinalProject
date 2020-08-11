using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkoutFollowUI : MonoBehaviour
{
    public static WorkoutFollowUI Instance { get; set; }

    public GameObject ExerciseUI;
    public GameObject RestUI;
    public GameObject ReadyUI;

    private int ExerciseNumber = 0;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UIManager.Instance.FadeIn();
        Debug.Log("FADE IN");

        //SET FIRST EXERCISE
        ExerciseUI.GetComponent<ExerciseFollowUI>().SetUpExercise(ExerciseNumber);
    }


    public void StartExerciseUI()
    {
        ReadyUI.GetComponent<UIScreen>().GoRight();
        ExerciseUI.GetComponent<UIScreen>().GoCenter();

        //START EXERCISE
        ExerciseUI.GetComponent<ExerciseFollowUI>().StartExercise();


        //SET UP REST TIME
        RestUI.GetComponent<RestUI>().SetupRest(ExerciseNumber);
    }

    public void SwitchToRestUI()
    {
        //IF LAST GO TO END SCREEN
        if (ExerciseNumber == UIManager.CurrentWorkout.Exercises.Count - 1)
        {
            //GO TO LAST SCREEN

            return;
        }



        //IF NO REST GO TO NEXT EXERCISE RIGHT AWAY
        if (UIManager.CurrentWorkout.Exercises[ExerciseNumber].RestTime == 0)
        {
            //SET UP NEXT EXERCISE
            ExerciseNumber++;
            ExerciseUI.GetComponent<ExerciseFollowUI>().SetUpExercise(ExerciseNumber);

            //START EXERCISE
            ExerciseUI.GetComponent<ExerciseFollowUI>().StartExercise();
            
            return;
        }

        
        RestUI.GetComponent<UIScreen>().GoCenter();
        ExerciseUI.GetComponent<UIScreen>().GoLeft();

        //START REST
        RestUI.GetComponent<RestUI>().StartRest();

        //SET UP NEXT EXERCISE
        ExerciseNumber++;
        ExerciseUI.GetComponent<ExerciseFollowUI>().SetUpExercise(ExerciseNumber);
    }

    public void SwitchToExerciseUI()
    {
        RestUI.GetComponent<UIScreen>().GoRight();
        ExerciseUI.GetComponent<UIScreen>().GoCenter();

        //START EXERCISE
        ExerciseUI.GetComponent<ExerciseFollowUI>().StartExercise();

        //SET UP REST TIME
        RestUI.GetComponent<RestUI>().SetupRest(ExerciseNumber);
    }


    public void GoBack()
    {
        if (ExerciseNumber > 0)
        {
            ExerciseNumber--;
            ExerciseUI.GetComponent<ExerciseFollowUI>().SetUpExercise(ExerciseNumber);
            ExerciseUI.GetComponent<ExerciseFollowUI>().StartExercise();
            RestUI.GetComponent<RestUI>().SetupRest(ExerciseNumber);
        }
    }
}
