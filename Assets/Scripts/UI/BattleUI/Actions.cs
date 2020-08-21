using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Actions : MonoBehaviour
{
    private float DestinationScale;
    private float OriginScale;
    private bool LerpScale;

    public float LerpDuration;
    private float StartLerpTime;

    public float NormalScale;

    public List<Action> ActionButtons;

    public void Appear(CharacterUnit c)
    {
        List<Skill> skills = c.GetSkills();

        for(int i = 0; i < skills.Count; i++)
        {
            ActionButtons[i].SetSkill(c, skills[i]);
        }

        StartLerpTime = Time.time;

        LerpScale = true;
        OriginScale = this.transform.localScale.x;
        DestinationScale = NormalScale;

    }

    


    public void Disappear()
    {
        foreach (Action a in ActionButtons)
        {
            a.interactable = false;
        }
        StartLerpTime = Time.time;


        LerpScale = true;
        OriginScale = this.transform.localScale.x;
        DestinationScale = 0;

    }


    void Update()
    {
        

        if (LerpScale)
        {

            float Progress = Time.time - StartLerpTime;

            float ProgressClamp = Mathf.Clamp(Progress / LerpDuration, 0, 1);
            float Scale = Mathf.Lerp(OriginScale, DestinationScale, ProgressClamp);
            this.transform.localScale = new Vector3(Scale, Scale, Scale);
            if (LerpDuration < Progress)
            {
                this.transform.localScale = new Vector3(DestinationScale, DestinationScale, DestinationScale);

                LerpScale = false;
            }

        }

        
    }
}
