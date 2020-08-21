using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionAnimated : MonoBehaviour
{
    [SerializeField] private Progression Progression;
    private bool Animating;
    private int ExperienceToAnimate;
    private int ExperienceToAnimateAux;
    public float AnimationTime;

    private int Level;
    private int Experience;

    public Bar ExperienceBar;
    public Text LevelText;

    private float TimeSinceUpdate = 0f;

    public Text CharacterName;
    public GameObject Unlockable;
    public Text ExpGained;
    public Image CharacterImage;

    public void SetProgression(Character c, Progression p, Color cl)
    {
        Progression = p;
        CharacterName.text = c.CharacterName;
        ExperienceBar.SetColor(cl);
        CharacterImage.sprite = c.CharacterProfilePicture;

        Level = Progression.GetLevel();
        Experience = Progression.GetExperience();

        ExperienceBar.ChangeCurrentMax(Experience, Progression.GetExperienceToNextLevel(Level));
        LevelText.text = Level.ToString();

        Progression.OnExperienceChanged += ProgressionOnExperienceChanged;
        Progression.OnLevelChanged += ProgressionOnLevelChanged;

        
    }

    private void ProgressionOnLevelChanged(object sender, EventArgs e)
    {
        Animating = true;
    }

    private void ProgressionOnExperienceChanged(object sender, EventArgs e)
    {
        ExperienceArgs ex = e as ExperienceArgs;
        Animating = true;
        ExperienceToAnimate = ex.Amount;
        ExperienceToAnimateAux = ex.Amount;
        TimeSinceUpdate = 0f;
        Debug.Log(ExpGained);
        Debug.Log(ex);
        Debug.Log(ex.Amount);
        ExpGained.text = String.Format("+{0:0}EXP",ex.Amount);
    }

    private void Update()
    {
        Debug.Log(ExpGained);
        if (Animating)
        {
            if(Level < Progression.GetLevel())
            {
                //Animated Level under Target Level
                AddExperience();
            } else
            {
                //Animated Level equals Target Level
                if (!Progression.IsMaxLevel(Level) && Experience < Progression.GetExperience())
                {
                    AddExperience();
                } else
                {
                    Animating = false;
                }
            }

            TimeSinceUpdate += Time.deltaTime;
        }


        
    }

    private void OnDestroy()
    {
        Progression.OnExperienceChanged -= ProgressionOnExperienceChanged;
        Progression.OnLevelChanged -= ProgressionOnLevelChanged;
    }

    private void AddExperience()
    {
        
        //Calculate how much experience to increase during this frame
        //In order for the animation to always last the same amount of time,
        //the number of experience to gain per frame is calculate based on
        //TotalExperienceGained * TimeSinceLastFrameUpdate / AnimationTime
        int EXPPERFRAME = Mathf.RoundToInt(ExperienceToAnimate * TimeSinceUpdate / AnimationTime);


        if (EXPPERFRAME > 0)
        {            

            while (EXPPERFRAME > 0)
            {
                Experience++;
                EXPPERFRAME--;

                if (Progression.IsMaxLevel(Level))
                {
                    ExperienceBar.ChangeCurrentMax(Progression.GetExperienceToNextLevel(Level - 1), Progression.GetExperienceToNextLevel(Level - 1));
                    return;
                }
                else if (Experience >= Progression.GetExperienceToNextLevel(Level))
                {
                    Level++;
                    Experience = 0;
                    //ADD EVENT HANDLER, ON LEVEL CHANGED
                    Unlockable.SetActive(true);
                    if (!Progression.IsMaxLevel(Level))
                    {
                        LevelText.text = Level.ToString();
                        ExperienceBar.ChangeCurrentMax(Experience, Progression.GetExperienceToNextLevel(Level));
                    } else
                    {
                        LevelText.text = "MAX";
                        ExperienceBar.ChangeCurrentMax(1, 1);
                    }

                }

                //ADD EVENT HANDLER, ON EXPERIENCE CHANGED
            }

            //Change the LerpDuration based on this time;
            ExperienceBar.LerpDuration = TimeSinceUpdate;
            ExperienceBar.ChangeCurrentValueFlat(Experience);

            //Reset TimeSinceLastFrameUpdate
            TimeSinceUpdate = 0;
        }



    }
}
