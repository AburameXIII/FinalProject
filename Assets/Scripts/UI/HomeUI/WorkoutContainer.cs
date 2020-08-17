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
    public GameObject Locked;
    public Text LockedText;

    private Workout Workout;
    private Character Character;

    public void UpdateWorkout(Character c, WorkoutUnlock w)
    {
        CharacterPicture.sprite = c.CharacterProfilePicture;
        WorkoutName.text = w.Workout.WorkoutName;
        WorkoutDuration.text = w.Workout.EstimatedDuration.ToString() + " mins";



        for(int i = 0; i < w.Workout.Difficulty; i++)
        {
            StarDifficulty[i].color = Color.yellow;
        }


        int FightLevel = PartyManager.Instance.GetFightProgression(c).GetLevel();
        if(FightLevel < w.Level)
        {
            Locked.SetActive(true);
            LockedText.text = "Reach Fight level " + w.Level + " to unlock this";
        } else
        {
            Locked.SetActive(false);
            ToExercise.onClick.AddListener(delegate { UIManager.Instance.SwitchToExerciseUI(c, w.Workout); });
        }

        Workout = w.Workout;
        Character = c;
    }
}
