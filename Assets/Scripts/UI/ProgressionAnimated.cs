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

    public void TEST()
    {
        SetProgression(new Progression());
    }

    public void TEST2()
    {
        Progression.AddExperience(1000);
    }



    public void SetProgression(Progression p)
    {
        Progression = p;

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
    }

    private void Update()
    {
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

                    LevelText.text = Level.ToString();
                    if (!Progression.IsMaxLevel(Level))
                    {
                        ExperienceBar.ChangeCurrentMax(Experience, Progression.GetExperienceToNextLevel(Level));
                    }

                }

                //ADD EVENT HANDLER, ON EXPERIENCE CHANGED
            }

            //Change the LerpDuration based on this time;
            ExperienceBar.LerpDuration = TimeSinceUpdate;
            ExperienceBar.ChangeCurrentValue(Experience);

            //Reset TimeSinceLastFrameUpdate
            TimeSinceUpdate = 0;
        }



    }
}
