using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set; }

    public static Workout CurrentWorkout;
    public static Character CurrentWorkoutCharacter;
    public static Level CurrentLevel;
    public static List<MeasuringMethod> ChosenMeasuringMethods = new List<MeasuringMethod>();

    public UIScreen HomeUI;
    public UIScreen WorkoutUI;
    public UIScreen ExerciseUI;
    public UIScreen LevelUI;

    public Camera HomeCamera;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    public void SwitchToWorkoutUI()
    {
        WorkoutUI.GetComponent<WorkoutUI>().UpdateUI();
        HomeUI.GoRight();
        WorkoutUI.GoCenter();
    }

    public void SwitchToHomeUI()
    {
        HomeUI.GoCenter();
        WorkoutUI.GoLeft();
    }

    public void SwitchToLevelUI()
    {
        HomeUI.GoLeft();
        LevelUI.GoCenter();
    }

    public void BackLevelUI()
    {
        HomeUI.GoCenter();
        LevelUI.GoRight();
    }

    public void SwitchToExerciseUI(Character c, Workout w)
    {
        WorkoutUI.GoRight();
        ExerciseUI.GetComponent<ExercisesUI>().UpdateUI(c, w);
        ExerciseUI.GoCenter();
    }

    public void SwitchBackToWorkoutUI()
    {
        ExerciseUI.GoLeft();
        WorkoutUI.GoCenter();
    }

    public void StartWorkout(Character c, Workout w)
    {
        Fader.raycastTarget = true;
        CurrentWorkout = w;
        CurrentWorkoutCharacter = c;
        StartCoroutine(ChangeToWorkoutScene());
    }

    public void FinishWorkout()
    {
        WorkoutUI.GoLeft(0.01f);
        ExerciseUI.GoLeft(0.01f);
        HomeUI.GoCenter(0.01f);
        Fader.raycastTarget = true;
        StartCoroutine(ChangeToHomeScene());
    }

    IEnumerator ChangeToWorkoutScene()
    {
        Lerp = true;
        Origin = new Color(0.87f, 0.87f, 0.87f, 0);
        Destination = new Color(0.87f, 0.87f, 0.87f, 1);
        StartLerpTime = Time.time;
        yield return new WaitForSeconds(LerpSceneChangeDuration);

        //CHANGE SCENES
        DisableHomeUI();
        SceneManager.LoadScene("Workout", LoadSceneMode.Additive);
        HomeCamera.cullingMask = (1 << LayerMask.NameToLayer("Workout")) | (1 << LayerMask.NameToLayer("Fader"));
        
    }

    private void ActivateHomeUI()
    {
        HomeUI.gameObject.SetActive(true);
        WorkoutUI.gameObject.SetActive(true);
        ExerciseUI.gameObject.SetActive(true);
    }

    private void DisableHomeUI()
    {
        HomeUI.gameObject.SetActive(false);
        WorkoutUI.gameObject.SetActive(false);
        ExerciseUI.gameObject.SetActive(false);
    }


    public void LoadLevel(Level l)
    {
        Fader.raycastTarget = true;
        CurrentLevel = l;
        StartCoroutine(ChangeToFightScene());
    }

    IEnumerator ChangeToFightScene()
    {
        Lerp = true;
        Origin = new Color(0.07f, 0.07f, 0.07f, 0);
        Destination = new Color(0.07f, 0.07f, 0.07f, 1);
        StartLerpTime = Time.time;
        yield return new WaitForSeconds(LerpSceneChangeDuration);

        //CHANGE SCENES
        DisableHomeUI();
        SceneManager.LoadScene("Fight", LoadSceneMode.Additive);
        HomeCamera.cullingMask = (1 << LayerMask.NameToLayer("Fight")) | (1 << LayerMask.NameToLayer("Fader"));

    }

    IEnumerator ChangeToHomeScene()
    {
        Lerp = true;

        Origin = new Color(0.87f, 0.87f, 0.87f, 0);
        Destination = new Color(0.87f, 0.87f, 0.87f, 1);
        StartLerpTime = Time.time;
        yield return new WaitForSeconds(LerpSceneChangeDuration);

        //CHANGE SCENES
        ActivateHomeUI();
        HomeUI.GetComponent<HomeUI>().ShowParty();
        HomeCamera.cullingMask = ~(1 << LayerMask.NameToLayer("Workout"));
        SceneManager.UnloadSceneAsync("Workout");
        FadeIn();

    }

    public void FinishLevel()
    {
        HomeUI.GoCenter(0.01f);
        LevelUI.GoRight(0.01f);
        Fader.raycastTarget = true;
        StartCoroutine(FinishLevelCoroutine());
    }

    IEnumerator FinishLevelCoroutine()
    {
        Lerp = true;

        Origin = new Color(0.87f, 0.87f, 0.87f, 0);
        Destination = new Color(0.87f, 0.87f, 0.87f, 1);
        StartLerpTime = Time.time;
        yield return new WaitForSeconds(LerpSceneChangeDuration);

        //CHANGE SCENES
        ActivateHomeUI();
        HomeUI.GetComponent<HomeUI>().ShowParty();
        HomeCamera.cullingMask = ~(1 << LayerMask.NameToLayer("Fight"));
        SceneManager.UnloadSceneAsync("Fight");
        FadeIn();

    }

    public void FadeIn()
    {
        Lerp = true;
        if (Origin != null)
        {
            Destination = Origin;
            Origin.a = 1;
        } else
        {
            Origin = new Color(0.87f, 0.87f, 0.87f, 1);
            Destination = new Color(0.87f, 0.87f, 0.87f, 0);
        }
        StartLerpTime = Time.time;
        Fader.raycastTarget = false;
    }

    private bool Lerp;
    public float LerpSceneChangeDuration;
    private float StartLerpTime;
    private Color Destination;
    private Color Origin;
    public Image Fader;

    void Update()
    {
        if (Lerp)
        {
            float Progress = Time.time - StartLerpTime;

            Fader.color = Color.Lerp(Origin, Destination, Progress / LerpSceneChangeDuration);

            if (LerpSceneChangeDuration < Progress)
            {
                Lerp = false;
            }
        }
    }
}
