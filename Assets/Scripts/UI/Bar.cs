using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour, IWorkoutObjective
{
    public float MaxValue;
    public float TargetValue;
    public float LerpDuration;
    private float InitialValue;
    private bool lerpNow;
    public float CurrentValue;

    private float startLerp;

    public Image LerpValueBar;

    public Text BindText;
    public string TextFormat;
    public bool Countdown;

    void Update()
    {
        if (lerpNow)
        {
            float Progress = Time.time - startLerp;
            CurrentValue = Mathf.Lerp(InitialValue, TargetValue, Progress / LerpDuration);
            LerpValueBar.fillAmount = CurrentValue / MaxValue;
            if (LerpDuration < Progress)
            {
                lerpNow = false;
            }
        }
        ChangeText();
            
    }

    void ChangeText()
    {
        if (BindText != null)
        {
            if (Countdown)
            {
                int TotalSeconds = Mathf.Clamp(Mathf.RoundToInt(MaxValue - CurrentValue), 0, Mathf.RoundToInt(MaxValue));
                int Minutes = TotalSeconds / 60;
                int Seconds = TotalSeconds % 60;
                BindText.text = string.Format("{0:D2}:{1:D2}", Minutes, Seconds);
            }
            else
            {
                BindText.text = string.Format(TextFormat, CurrentValue, MaxValue);
            }

        }
    }

    public void ChangeCurrentValue(float newValue)
    {
        InitialValue = LerpValueBar.fillAmount * MaxValue;
        lerpNow = true;
        startLerp = Time.time;
        TargetValue = newValue;
    }

    public void ChangeCurrentValueFlat(float newValue)
    {
        TargetValue = newValue;
        CurrentValue = newValue;
        LerpValueBar.fillAmount = CurrentValue / MaxValue;
        ChangeText();
    }


    public void ChangeCurrentMax(float Current, float Max)
    {
        MaxValue = Max;
        TargetValue = Current;
        CurrentValue = Current;
        LerpValueBar.fillAmount = Current / Max;
        ChangeText();
    }

    

    public void SetTextForm(string Format)
    {
        TextFormat = Format;
    }

    public void SetColor(Color c)
    {
        LerpValueBar.color = c;
    }


    public void Setup(float Max)
    {
        Countdown = true;
        MaxValue = Max;
        TargetValue = Max;
        InitialValue = 0;
        CurrentValue = 0;
        LerpValueBar.fillAmount = 0;
        LerpDuration = Max;
    }


    public void StartMeasuring()
    {

        startLerp = Time.time;
        lerpNow = true;
    }


    public bool IsCompleted()
    {
        if (InitialValue >= TargetValue)
            return CurrentValue <= TargetValue;
        else
            return CurrentValue >= TargetValue;
    }

    public void Stop()
    {
        Debug.Log("STOPPING");
        lerpNow = false;
    }

}