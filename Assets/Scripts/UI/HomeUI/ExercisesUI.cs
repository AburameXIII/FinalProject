using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExercisesUI : MonoBehaviour
{
    public RectTransform ExerciseContainer;
    public GameObject ExercisePrefab;

    public Text WorkoutNameText;
    public Text DetailText;
    public Image WorkoutCharacter;
    public Button StartWorkoutButton;

    public float StartY;
    public float YDistance;


    private List<GameObject> Exercises = new List<GameObject>();

    public void UpdateUI(Character c, Workout w)
    {
        WorkoutCharacter.sprite = c.CharacterProfilePicture;
        WorkoutNameText.text = w.WorkoutName;
        DetailText.text = w.EstimatedDuration + " mins - " + w.Exercises.Count + " exercises";
        StartWorkoutButton.onClick.RemoveAllListeners();
        StartWorkoutButton.onClick.AddListener(delegate { UIManager.Instance.StartWorkout(c,w); });


        if(Exercises.Count != 0)
        {
            foreach (GameObject o in Exercises)
            {
                Destroy(o);
            }
        }
        

        int count = 0;
        float Y = StartY;

        UIManager.ChosenMeasuringMethods.Clear();

        foreach (Exercise e in w.Exercises)
        {
                var newExerciseUI = Instantiate(ExercisePrefab, Vector3.zero, Quaternion.identity);
                newExerciseUI.transform.SetParent(ExerciseContainer.transform, false);
                newExerciseUI.transform.localPosition = new Vector3(0, Y, 0);

                MeasuringMethod m = e.PossibleMeasuringMethod[e.PossibleMeasuringMethod.Count - 1];

                newExerciseUI.GetComponent<ExerciseContainer>().UpdateExercise(e, m);

                UIManager.ChosenMeasuringMethods.Add(m);
                Exercises.Add(newExerciseUI);
                Y -= YDistance;
                count++;
        }

        ExerciseContainer.sizeDelta = new Vector2(ExerciseContainer.sizeDelta.x, count * YDistance + 50);

    }

    
}
