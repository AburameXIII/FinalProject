using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkoutContainer : MonoBehaviour
{
    public Image CharacterPicture;
    public Text WorkoutName;
    public List<Image> StarDifficulty;
    public Text WorkoutDuration;
    public Button ToExercise;

    private Workout Workout;
    private Character Character;

    public void UpdateWorkout(Character c, Workout w)
    {
        CharacterPicture.sprite = c.CharacterProfilePicture;
        WorkoutName.text = w.WorkoutName;
        WorkoutDuration.text = w.EstimatedDuration.ToString() + " mins";
        for(int i = 0; i < w.Difficulty; i++)
        {
            StarDifficulty[i].color = Color.yellow;
        }

        ToExercise.onClick.AddListener(delegate { UIManager.Instance.SwitchToExerciseUI(c,w); });

        Workout = w;
        Character = c;
    }
}
