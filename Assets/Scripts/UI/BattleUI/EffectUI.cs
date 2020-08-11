using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectUI : MonoBehaviour
{
    public Image Background;
    public Image EffectSprite;
    public Text EffectStack;

    private Vector3 DestinationPosition;
    private Vector3 OriginPosition;
    private bool LerpPosition;

    private Vector3 DestinationScale;
    private Vector3 OriginScale;
    private bool LerpScale;

    private Color DestinationSpriteColor;
    private Color DestinationBackgroundColor;
    private Color DestinationTextColor;
    private Color OriginSpriteColor;
    private Color OriginBackgroundColor;
    private Color OriginTextColor;
    private bool LerpColor;
    
    public float LerpDuration;
    private float StartLerpTime;
    private bool Delete;

    public int amount;
    public Color BackgroundColor;
    public Color SpriteColor;

    private void Start()
    {
        Delete = false;
    }

    public void SetSprite(SkillEffect e)
    {

        EffectSprite.sprite = e.SkillSprite;
        amount = 1;
    }

    public void UpdateStack()
    {
        if(amount == 1)
        {
            EffectStack.gameObject.SetActive(false);
        } else
        {
            EffectStack.text = amount.ToString();
            EffectStack.gameObject.SetActive(true);
        }
    }

    public void GoDown()
    {
        StartLerpTime = Time.time;

        LerpPosition = true;
        OriginPosition = this.transform.position;
        DestinationPosition = this.transform.position - new Vector3(0,-100,0);

        LerpColor = true;

        OriginSpriteColor = SpriteColor;
        OriginBackgroundColor = BackgroundColor;
        OriginTextColor = Color.white;
        DestinationSpriteColor = OriginSpriteColor;
        DestinationSpriteColor.a = 0;
        DestinationBackgroundColor = OriginBackgroundColor;
        DestinationBackgroundColor.a = 0;
        DestinationTextColor = OriginTextColor;
        DestinationTextColor.a = 0;

        Delete = true;
    }

    public void GoIn(Transform t)
    {
        StartLerpTime = Time.time;

        LerpPosition = true;
        OriginPosition = this.transform.position;
        DestinationPosition = t.position;

        LerpColor = true;

        DestinationSpriteColor = SpriteColor;
        DestinationBackgroundColor = BackgroundColor;
        DestinationTextColor = Color.white;
        OriginSpriteColor = DestinationSpriteColor;
        OriginSpriteColor.a = 0;
        OriginBackgroundColor = DestinationBackgroundColor;
        OriginBackgroundColor.a = 0;
        OriginTextColor = DestinationTextColor;
        OriginTextColor.a = 0;
    }

    public void GoNext(Transform t)
    {
        StartLerpTime = Time.time;

        LerpPosition = true;
        OriginPosition = this.transform.position;
        DestinationPosition = t.position;
    }


    void Update()
    {
        if (LerpPosition)
        {
            float Progress = Time.time - StartLerpTime;
            float ProgressClamp = Mathf.Clamp(Progress / LerpDuration, 0, 1);
            this.transform.position = Vector3.Lerp(OriginPosition, DestinationPosition, ProgressClamp);
            if (LerpDuration < Progress)
            {
                //this.transform.position = DestinationPosition;
                LerpPosition = false;
                if (Delete)
                {
                    Destroy(this.gameObject);
                }
            }
        }


        if (LerpScale)
        {

            float Progress = Time.time - StartLerpTime;

            float ProgressClamp = Mathf.Clamp(Progress / LerpDuration, 0, 1);
            this.transform.localScale = Vector3.Lerp(OriginScale, DestinationScale, ProgressClamp);
            if (LerpDuration < Progress)
            {
                //this.transform.localScale = DestinationScale;
                LerpScale = false;
            }

        }

        if (LerpColor)
        {
            float Progress = Time.time - StartLerpTime;

            float ProgressClamp = Mathf.Clamp(Progress / LerpDuration, 0, 1);

            EffectSprite.color = Color.Lerp(OriginSpriteColor, DestinationSpriteColor, ProgressClamp);
            Background.color = Color.Lerp(OriginBackgroundColor, DestinationBackgroundColor, ProgressClamp);
            EffectStack.color = Color.Lerp(OriginTextColor, DestinationTextColor, ProgressClamp);

            if (LerpDuration < Progress)
            {
                LerpColor = false;
            }
        }

    }

    
}
