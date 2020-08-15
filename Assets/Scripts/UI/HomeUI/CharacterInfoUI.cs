using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoUI : MonoBehaviour
{
    public Text CharacterName;
    public Text FightNumber;
    public Text TrainNumber;
    public Bar FightBar;
    public Bar TrainBar;

    public void Setup(Character c)
    {
        CharacterName.text = c.CharacterName;
        Progression F = PartyManager.Instance.GetFightProgression(c);
        if (F.IsMaxLevel())
        {
            FightNumber.text = "MAX";
            FightBar.ChangeCurrentMax(1, 1);
        } else
        {
            FightNumber.text = F.GetLevel().ToString();
            FightBar.ChangeCurrentMax(F.GetExperience(), F.GetExperienceToNextLevel());
        }
        FightBar.SetColor(c.CharacterSecondaryColor);
        

        Progression T = PartyManager.Instance.GetTrainProgression(c);
        if (T.IsMaxLevel())
        {
            TrainNumber.text = "MAX";
            FightBar.ChangeCurrentMax(1, 1);
        }
        else
        {
            TrainNumber.text = T.GetLevel().ToString();
            TrainBar.ChangeCurrentMax(T.GetExperience(), T.GetExperienceToNextLevel());
        }
        TrainBar.SetColor(c.CharacterPrimaryColor);
        

    }

}
