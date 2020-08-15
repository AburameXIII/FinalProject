using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Progression
{
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;
    [SerializeField] private int Level;
    [SerializeField] private int Experience;

    private static readonly int[] ExperiencePerLevel = new[] { 100, 120, 140, 160, 180, 200, 400, 1000 };

    public Progression()
    {
        Level = 1;
        Experience = 0;
    }

    public void AddExperience(int Amount)
    {
        if (!IsMaxLevel()) { 
            Experience += Amount;
            while(!IsMaxLevel() && Experience >= GetExperienceToNextLevel(Level))
            { 
                Experience -= GetExperienceToNextLevel(Level);
                Level++;
                if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
            }
            if (OnExperienceChanged != null) OnExperienceChanged(this, new ExperienceArgs(Amount));
        }
    }

    public int GetLevel()
    {
        return Level;
    }

    public int GetExperience()
    {
        return Experience;
    }

    public int GetExperienceToNextLevel(int Level)
    {
        if(Level <= ExperiencePerLevel.Length)
        {
            return ExperiencePerLevel[Level - 1];
        } else
        {
            //Invalid level
            Debug.LogError("Level invalid:" + Level);
            return 0;
        }
        
    }

    public int GetExperienceToNextLevel()
    {
        if (Level <= ExperiencePerLevel.Length)
        {
            return ExperiencePerLevel[Level - 1];
        }
        else
        {
            //Invalid level
            Debug.LogError("Level invalid:" + Level);
            return 0;
        }

    }

    public bool IsMaxLevel()
    {
        return IsMaxLevel(Level);
    }

    public bool IsMaxLevel(int Level)
    {
        return Level == ExperiencePerLevel.Length + 1;
    }

    public float GetExperienceNormalized()
    {
        if (IsMaxLevel()) return 1f;
        return (float)Experience / GetExperienceToNextLevel(Level);
    }
}

public class ExperienceArgs : System.EventArgs
{
    public ExperienceArgs (int Amount)
    {
        this.Amount = Amount;
    }
    public int Amount { get; private set; }
}
