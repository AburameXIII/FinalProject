using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyUI : MonoBehaviour
{
    public Bar CountdownBar;
    public float CountdownTime;
    private bool Switch;

    public Button ExitButton;

    private void Awake()
    {
        CountdownBar.SetColor(UIManager.CurrentWorkoutCharacter.CharacterColor);
        Switch = true;
        ExitButton.onClick.AddListener(delegate { UIManager.Instance.FinishWorkout(); });

        CountdownBar.Setup(CountdownTime);
    }

    void Start()
    {
        CountdownBar.StartMeasuring();
    }


    void Update()
    {
        if (Switch && CountdownBar.IsCompleted())
        {
            WorkoutFollowUI.Instance.StartExerciseUI();
            Switch = false;
        }
    }

    public void StartExerciseUI()
    {
        WorkoutFollowUI.Instance.StartExerciseUI();
        Switch = false;
    }
}
